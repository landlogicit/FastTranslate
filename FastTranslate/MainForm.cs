/*
 * FastTranslate - This software is useful when translating language XML files, providing reference language and searchability.
 * Copyright (C) 2012-2014 Markus Kvist, StjärnDistribution AB
 * Contact: markus.kvist@sdist.se
 * 
 * This program is free software; you can redistribute it and/or modify it 
 * under the terms of the GNU General Public License as published by the Free Software Foundation; 
 * either version 2 of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program; 
 * if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using FastTranslate.ResourceFiles;
using FastTranslate.Suggestions;

namespace FastTranslate
{
    public partial class MainForm : Form
    {
        #region Private variables

        private static readonly string[] InvalidLeafResources =
        {
            "Hint", "Text", "Title", "ErrorMessage", "Required"
        };

        private DataTable _table;
        private Dictionary<string, DataRow> _rows;
        private string _searchText;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            InitializeVariables();
        }

        #endregion

        #region Event handlers

        private void openReferenceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(1);
        }

        private void openFileToTranslateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(2);
        }

        private void saveTranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave(false);
        }

        private void saveTranslationAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave(true);
        }

        private void openFile3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Value3.Visible = true;
            OpenFile(3);
        }

        private void openFile4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Value4.Visible = true;
            OpenFile(4);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFind();
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindNext(false);
        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindNext(true);
        }

        private void removeOrphanLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveOrphanLines();
        }

        private void fillInTheBlanksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillInTheBlanks();
        }

        private void loadSuggestionReferenceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSuggestionReferenceFile();
        }

        private void addCurrentTextsToSuggestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCurrentTextsToSuggestion();
        }

        private void suggestTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuggestText();
        }

        private void displaySuggestionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplaySuggestionInfo(dataGridView1.CurrentCell);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BeginEdit(true);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.X)
                {
                    // Cut
                    if (!dataGridView1.IsCurrentCellInEditMode)
                    {
                        // Get selected cells to clipboard
                        Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                        // Clear selected cells
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            if (!cell.ReadOnly)
                                cell.Value = string.Empty;
                        }
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.V)
                {
                    // Paste
                    if (dataGridView1.IsCurrentCellInEditMode && dataGridView1.SelectedCells.Count <= 1)
                        return;
                    PasteClipboard();
                    e.Handled = true;
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if ((e.Modifiers & Keys.Control) == Keys.Control)
                {
                    if (!dataGridView1.IsCurrentCellInEditMode ||
                        dataGridView1.EndEdit())
                    {
                        bool findPrevious = (e.Modifiers & Keys.Shift) == Keys.Shift;
                        FindNextToTranslate(findPrevious);
                        e.Handled = true;
                    }
                }
            }
            if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    // Clear selected cells
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (!cell.IsInEditMode && !cell.ReadOnly)
                        {
                            cell.Value = string.Empty;
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3) // Value 2
            {
                if (Convert.ToString(e.Value) == string.Empty)
                    e.CellStyle.BackColor = Color.Beige;
                else
                    e.CellStyle.BackColor = Color.White;
            }
        }

        #endregion

        #region Private methods

        private readonly ResourceSuggester _resourceSuggester = new ResourceSuggester();

        private void InitializeVariables()
        {
            _rows = new Dictionary<string, DataRow>();
            _table = new DataTable();
            _table.Columns.Add("ResourceName", typeof(string));
            _table.Columns.Add("LeafResource", typeof(string));
            _table.Columns.Add("Value1", typeof(string));
            _table.Columns.Add("Value2", typeof(string));
            _table.Columns.Add("Value3", typeof(string));
            _table.Columns.Add("Value4", typeof(string));
            bindingSource1.DataSource = _table;
            _resourceSuggester.AddInvalidLeafNames(InvalidLeafResources);
        }

        #region Load/save

        private void OpenFile(int columnNumber)
        {
            string fileName = GetFileFromUser("Open file");
            if (fileName != null)
                ReadFile(fileName, columnNumber);
            // Save opened file
            if (columnNumber == 2)
                _currentFileName = fileName;
        }

        private void ReadFile(string fileName, int columnNumber)
        {
            var reader = new ResourceFileReader();
            ResourceFile resourceFile = reader.ReadXmlFile(fileName);
            _table.BeginLoadData();
            string valueColumn = string.Format("Value{0}", columnNumber);
            dataGridView1.Columns[valueColumn].HeaderText = resourceFile.LanguageName;
            ReadLocaleResourceElementsRecursive(resourceFile, valueColumn);
            _table.EndLoadData();
        }

        private void ReadLocaleResourceElementsRecursive(ResourceFile resourceFile, string valueColumn)
        {
            foreach (Resource localeResource in resourceFile.Resources)
            {
                AddValue(valueColumn, localeResource.Name, localeResource.Text);
            }
        }

        private void AddValue(string valueColumn, string resourceName, string value)
        {
            DataRow row;
            // Find any existing resource name
            var resourceNameLowered = resourceName.ToLower();
            bool exists = _rows.TryGetValue(resourceNameLowered, out row);
            if (!exists)
            {
                // Create row if it does not exist
                row = _table.NewRow();
                row["ResourceName"] = resourceName;
                row["LeafResource"] = GetLeafResource(resourceName);
            }
            // Insert value
            row[valueColumn] = value;
            if (exists)
                return;
            // Insert row if it did not exist
            _rows.Add(resourceNameLowered, row);
            _table.Rows.Add(row);
        }

        private static string GetLeafResource(string resourceName)
        {
            string[] resourceNameSplit = resourceName.Split('.');
            string leaf = resourceNameSplit.Last();
            if (InvalidLeafResources.Contains(leaf) && resourceNameSplit.Length > 1)
                leaf = string.Format("{0}.{1}", resourceNameSplit[resourceNameSplit.Length - 2], leaf);
            return leaf;
        }

        private string _currentFileName;

        private void DoSave(bool saveAs)
        {
            if (saveAs || string.IsNullOrEmpty(_currentFileName))
            {
                if (string.IsNullOrEmpty(saveFileDialog1.InitialDirectory))
                    saveFileDialog1.InitialDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                DialogResult dialogResult = saveFileDialog1.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;
                _currentFileName = saveFileDialog1.FileName;
            }
            SaveFile(_currentFileName, 2);
        }

        private void SaveFile(string fileName, int columnNumber)
        {
            // This is what we are reading:
            //<Language Name="English">
            //  <LocaleResource Name="AboutUs">
            //    <Value>About us</Value>
            //  </LocaleResource>

            string valueColumn = string.Format("Value{0}", columnNumber);
            string languageName = dataGridView1.Columns[valueColumn].HeaderText;
            var languageElement = new XElement("Language", new XAttribute("Name", languageName));
            foreach (DataRow currentRow in _table.Rows)
            {
                var name = (string)currentRow["ResourceName"];
                string value = Convert.ToString(currentRow[valueColumn]);
                if (!string.IsNullOrEmpty(value))
                {
                    languageElement.Add(
                        new XElement("LocaleResource",
                            new XAttribute("Name", name),
                            new XElement("Value", value)));
                }
            }
            var document = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                languageElement);
            document.Save(fileName, SaveOptions.None);
        }

        #endregion

        #region Suggestions

        private string GetFileFromUser(string title)
        {
            openFileDialog1.Title = title;
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return null;
            return openFileDialog1.FileName;
        }

        private void LoadSuggestionReferenceFile()
        {
            var reader = new ResourceFileReader();

            string referenceFileName = GetFileFromUser("Select the file containing reference strings for suggestions");
            if (referenceFileName == null)
                return;
            string translatedFileName = GetFileFromUser("Select the file containing translated strings for suggestions");
            if (translatedFileName == null)
                return;

            ResourceFile referenceFile = reader.ReadXmlFile(referenceFileName);
            ResourceFile translatedFile = reader.ReadXmlFile(translatedFileName);

            foreach (Resource referenceResource in referenceFile.Resources)
            {
                Resource translatedResource = translatedFile.FindResourceByName(referenceResource.Name);
                _resourceSuggester.AddResource(
                    new TranslatedResource(referenceResource.Name, referenceResource.Text, translatedResource.Text));
            }
        }

        private void AddCurrentTextsToSuggestion()
        {
            _resourceSuggester.AddResources(GetTranslatedResources());
        }

        private IEnumerable<TranslatedResource> GetTranslatedResources()
        {
            foreach (DataRow currentRow in _table.Rows)
            {
                var name = (string)currentRow["ResourceName"];
                string text = Convert.ToString(currentRow[Value1.Name]);
                string translatedText = Convert.ToString(currentRow[Value2.Name]);
                if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(translatedText))
                    continue;
                yield return new TranslatedResource(name, text, translatedText);
            }
        }

        private void SuggestText()
        {
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                SuggestTextFor(selectedCell);
            }
            if (dataGridView1.SelectedCells.Count > 1)
                return;
            // TODO: Only if successful.
            // TODO: Extract method
            if (dataGridView1.CurrentRow != null)
                dataGridView1.CurrentCell =
                    dataGridView1
                        .Rows[Math.Min(dataGridView1.CurrentRow.Index + 1, dataGridView1.Rows.Count - 1)]
                        .Cells[dataGridView1.CurrentCell.ColumnIndex];
        }

        private void SuggestTextFor(DataGridViewCell cell)
        {
            if (cell == null || cell.ReadOnly)
                return;
            if (cell.IsInEditMode)
                dataGridView1.EndEdit();
            Resource resource = GetResourceFrom(cell);
            _resourceSuggester.MinimumTextSimilarity = 0.5;
            ResourceSuggestion suggestion = _resourceSuggester.GetSuggestionFor(resource);
            if (suggestion == null)
                return;
            cell.Value = suggestion.Resource.TranslatedText;
        }

        private void DisplaySuggestionInfo(DataGridViewCell cell)
        {
            if (cell == null || cell.ReadOnly)
                return;
            Resource resource = GetResourceFrom(cell);
            _resourceSuggester.MinimumTextSimilarity = 0;
            IList<ResourceSuggestion> suggestions = _resourceSuggester.GetSuggestionsFor(resource);
            var text = new StringBuilder();
            text.AppendLine(string.Format("Search name: {0}", resource.Name));
            text.AppendLine(string.Format("Search text: {0}", resource.Text));
            text.AppendLine();
            text.AppendLine(string.Format("Number of suggestions: {0}", suggestions.Count));
            text.AppendLine();
            int count = 0;
            foreach (ResourceSuggestion resourceSuggestion in suggestions)
            {
                text.AppendLine(string.Format("{0}: {1}", count, resourceSuggestion));
                if (count++ > 20)
                    break;
            }
            Clipboard.SetText(text.ToString());
            MessageBox.Show(text.ToString(), "Suggestion info");
        }

        private Resource GetResourceFrom(DataGridViewCell cell)
        {
            string name = Convert.ToString(cell.OwningRow.Cells[ResourceName.Name].Value);
            string text = Convert.ToString(cell.OwningRow.Cells[Value1.Name].Value);
            return new Resource(name, text);
        }

        #endregion

        #region Find

        private void DoFind()
        {
            var findDialog = new FindDialog { SearchText = _searchText };
            DialogResult dialogResult = findDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string searchText = findDialog.SearchText;
                _searchText = searchText;
                FindNext(false);
            }
        }

        private void FindNext(bool reverse)
        {
            if (string.IsNullOrEmpty(_searchText))
                return;
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int rowCount = dataGridView1.Rows.Count;
            bool success = false;
            int direction = reverse ? -1 : 1;
            //for (int i = rowIndex + 1; i < rowCount; i++)
            int i = rowIndex + direction;
            while (true)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                DataGridViewCell cell = row.Cells[columnIndex];
                string value = Convert.ToString(cell.Value);
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.IndexOf(_searchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        dataGridView1.CurrentCell = cell;
                        success = true;
                        break;
                    }
                }
                i += direction;
                if (i >= rowCount)
                    i = 0;
                else if (i < 0)
                    i = rowCount - 1;
                if (i == rowIndex)
                    break; // Not found
            }
            if (!success)
            {
                MessageBox.Show(
                    "The program has searched all rows in the current column, and no further instances were found.",
                    "No more results");
            }
        }

        private void FindNextToTranslate(bool findPrevious)
        {
            bool success = false;
            int rowCount = dataGridView1.Rows.Count;
            if (rowCount > 1)
            {
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex; // 3
                int rowIndex = dataGridView1.CurrentCell != null ? dataGridView1.CurrentCell.RowIndex : 0;
                for (int i = rowIndex + (findPrevious ? -1 : 1); i != rowIndex;)
                {
                    if (i >= rowCount)
                        i = 0; // Wrap around to beginning
                    else if (i < 0)
                        i = rowCount - 1;
                    DataGridViewRow row = dataGridView1.Rows[i];
                    DataGridViewCell cell = row.Cells[columnIndex];
                    string value = Convert.ToString(cell.Value);
                    if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(Convert.ToString(row.Cells[2].Value)))
                    {
                        dataGridView1.CurrentCell = cell;
                        success = true;
                        break;
                    }
                    if (findPrevious)
                        i--;
                    else
                        i++;
                }
            }
            if (!success)
            {
                MessageBox.Show(
                    "The program has searched through all records in the current column and no further empty fields were found.",
                    "No more results");
            }
        }

        #endregion

        #region Tools

        private void PasteClipboard()
        {
            string[] lines = Clipboard.GetText().Replace("\r", "").Split('\n');
            List<string[]> data = lines.Select(line => line.Split('\t')).ToList();

            // Find top left corner
            int firstRow = int.MaxValue, firstColumn = int.MaxValue;
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                firstRow = Math.Min(firstRow, cell.RowIndex);
                firstColumn = Math.Min(firstColumn, cell.ColumnIndex);
            }

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.ReadOnly)
                    continue;
                int relativeRowIndex = cell.RowIndex - firstRow;
                int relativeColumnIndex = cell.ColumnIndex - firstColumn;
                int rowIndex = relativeRowIndex % data.Count;
                string[] sourceRow = data[rowIndex];
                int columnIndex = relativeColumnIndex % sourceRow.Length;
                string value = sourceRow[columnIndex];
                cell.Value = value;
            }
        }

        private void RemoveOrphanLines()
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you really want to delete all orphan lines?", "Delete",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int count = 0;
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    if (Convert.ToString(row.Cells[Value1.Name].Value) == string.Empty)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        count++;
                    }
                }
                MessageBox.Show(string.Format(
                    "{0} rows were removed.", count), "Remove orphan lines");
            }
        }

        private void FillInTheBlanks()
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you really want to fill all blanks?", "Fill blanks",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int count = 0;
                foreach (DataRow row in _table.Rows)
                {
                    if (Convert.ToString(row[3]) == string.Empty)
                    {
                        row[3] = Convert.ToString(row[2]);
                        //targetCell.Style.BackColor = Color.Moccasin;
                        count++;
                    }
                }
                MessageBox.Show(string.Format(
                    "{0} strings were copied.", count), "Fill blanks");
            }
        }

        #endregion
    }

    #endregion
}