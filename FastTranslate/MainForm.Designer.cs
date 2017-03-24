namespace FastTranslate
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ResourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeafResource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openReferenceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToTranslateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTranslationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTranslationAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openFile3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSuggestionReferenceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCurrentTextsToSuggestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.findNextemptyFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPreviousEmptyFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeOrphanLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillInTheBlanksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.suggestTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySuggestionInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ResourceName,
            this.LeafResource,
            this.Value1,
            this.Value2,
            this.Value3,
            this.Value4});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(13, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1062, 546);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // ResourceName
            // 
            this.ResourceName.DataPropertyName = "ResourceName";
            this.ResourceName.HeaderText = "Resource name";
            this.ResourceName.Name = "ResourceName";
            this.ResourceName.Width = 300;
            // 
            // LeafResource
            // 
            this.LeafResource.DataPropertyName = "LeafResource";
            this.LeafResource.HeaderText = "Leaf resource";
            this.LeafResource.Name = "LeafResource";
            this.LeafResource.ReadOnly = true;
            // 
            // Value1
            // 
            this.Value1.DataPropertyName = "Value1";
            this.Value1.HeaderText = "Language1";
            this.Value1.Name = "Value1";
            this.Value1.Width = 300;
            // 
            // Value2
            // 
            this.Value2.DataPropertyName = "Value2";
            this.Value2.HeaderText = "Language2";
            this.Value2.Name = "Value2";
            this.Value2.Width = 300;
            // 
            // Value3
            // 
            this.Value3.DataPropertyName = "Value3";
            this.Value3.HeaderText = "Language3";
            this.Value3.Name = "Value3";
            this.Value3.Visible = false;
            this.Value3.Width = 300;
            // 
            // Value4
            // 
            this.Value4.DataPropertyName = "Value4";
            this.Value4.HeaderText = "Language4";
            this.Value4.Name = "Value4";
            this.Value4.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML files|*.xml|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "XML files|*.xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1087, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openReferenceFileToolStripMenuItem,
            this.openFileToTranslateToolStripMenuItem,
            this.saveTranslationToolStripMenuItem,
            this.saveTranslationAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.openFile3ToolStripMenuItem,
            this.openFile4ToolStripMenuItem,
            this.toolStripSeparator2,
            this.loadSuggestionReferenceFileToolStripMenuItem,
            this.addCurrentTextsToSuggestionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openReferenceFileToolStripMenuItem
            // 
            this.openReferenceFileToolStripMenuItem.Name = "openReferenceFileToolStripMenuItem";
            this.openReferenceFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.openReferenceFileToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.openReferenceFileToolStripMenuItem.Text = "Open reference file...";
            this.openReferenceFileToolStripMenuItem.Click += new System.EventHandler(this.openReferenceFileToolStripMenuItem_Click);
            // 
            // openFileToTranslateToolStripMenuItem
            // 
            this.openFileToTranslateToolStripMenuItem.Name = "openFileToTranslateToolStripMenuItem";
            this.openFileToTranslateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToTranslateToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.openFileToTranslateToolStripMenuItem.Text = "Open file to translate...";
            this.openFileToTranslateToolStripMenuItem.Click += new System.EventHandler(this.openFileToTranslateToolStripMenuItem_Click);
            // 
            // saveTranslationToolStripMenuItem
            // 
            this.saveTranslationToolStripMenuItem.Name = "saveTranslationToolStripMenuItem";
            this.saveTranslationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveTranslationToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.saveTranslationToolStripMenuItem.Text = "Save translation";
            this.saveTranslationToolStripMenuItem.Click += new System.EventHandler(this.saveTranslationToolStripMenuItem_Click);
            // 
            // saveTranslationAsToolStripMenuItem
            // 
            this.saveTranslationAsToolStripMenuItem.Name = "saveTranslationAsToolStripMenuItem";
            this.saveTranslationAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveTranslationAsToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.saveTranslationAsToolStripMenuItem.Text = "Save translation as...";
            this.saveTranslationAsToolStripMenuItem.Click += new System.EventHandler(this.saveTranslationAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(345, 6);
            // 
            // openFile3ToolStripMenuItem
            // 
            this.openFile3ToolStripMenuItem.Name = "openFile3ToolStripMenuItem";
            this.openFile3ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.openFile3ToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.openFile3ToolStripMenuItem.Text = "Open file 3";
            this.openFile3ToolStripMenuItem.Click += new System.EventHandler(this.openFile3ToolStripMenuItem_Click);
            // 
            // openFile4ToolStripMenuItem
            // 
            this.openFile4ToolStripMenuItem.Name = "openFile4ToolStripMenuItem";
            this.openFile4ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.openFile4ToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.openFile4ToolStripMenuItem.Text = "Open file 4";
            this.openFile4ToolStripMenuItem.Click += new System.EventHandler(this.openFile4ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(345, 6);
            // 
            // loadSuggestionReferenceFileToolStripMenuItem
            // 
            this.loadSuggestionReferenceFileToolStripMenuItem.Name = "loadSuggestionReferenceFileToolStripMenuItem";
            this.loadSuggestionReferenceFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.loadSuggestionReferenceFileToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.loadSuggestionReferenceFileToolStripMenuItem.Text = "Load suggestion reference file pair...";
            this.loadSuggestionReferenceFileToolStripMenuItem.ToolTipText = resources.GetString("loadSuggestionReferenceFileToolStripMenuItem.ToolTipText");
            this.loadSuggestionReferenceFileToolStripMenuItem.Click += new System.EventHandler(this.loadSuggestionReferenceFileToolStripMenuItem_Click);
            // 
            // addCurrentTextsToSuggestionToolStripMenuItem
            // 
            this.addCurrentTextsToSuggestionToolStripMenuItem.Name = "addCurrentTextsToSuggestionToolStripMenuItem";
            this.addCurrentTextsToSuggestionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F12)));
            this.addCurrentTextsToSuggestionToolStripMenuItem.Size = new System.Drawing.Size(348, 22);
            this.addCurrentTextsToSuggestionToolStripMenuItem.Text = "Add translation to suggestions";
            this.addCurrentTextsToSuggestionToolStripMenuItem.ToolTipText = "Adds all the texts that have been translated so far to the suggestion dictionary." +
    " Useful if a text is recurring but not yet in the dictionary.";
            this.addCurrentTextsToSuggestionToolStripMenuItem.Click += new System.EventHandler(this.addCurrentTextsToSuggestionToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.findPreviousToolStripMenuItem,
            this.toolStripSeparator3,
            this.findNextemptyFieldToolStripMenuItem,
            this.findPreviousEmptyFieldToolStripMenuItem,
            this.toolStripSeparator4,
            this.removeOrphanLinesToolStripMenuItem,
            this.fillInTheBlanksToolStripMenuItem,
            this.toolStripSeparator5,
            this.suggestTextToolStripMenuItem,
            this.displaySuggestionInfoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.findNextToolStripMenuItem.Text = "Find next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
            // 
            // findPreviousToolStripMenuItem
            // 
            this.findPreviousToolStripMenuItem.Name = "findPreviousToolStripMenuItem";
            this.findPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.findPreviousToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.findPreviousToolStripMenuItem.Text = "Find previous";
            this.findPreviousToolStripMenuItem.Click += new System.EventHandler(this.findPreviousToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(296, 6);
            // 
            // findNextemptyFieldToolStripMenuItem
            // 
            this.findNextemptyFieldToolStripMenuItem.Name = "findNextemptyFieldToolStripMenuItem";
            this.findNextemptyFieldToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.findNextemptyFieldToolStripMenuItem.Text = "Find next &empty field (Ctrl-Enter)";
            // 
            // findPreviousEmptyFieldToolStripMenuItem
            // 
            this.findPreviousEmptyFieldToolStripMenuItem.Name = "findPreviousEmptyFieldToolStripMenuItem";
            this.findPreviousEmptyFieldToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.findPreviousEmptyFieldToolStripMenuItem.Text = "Find previous empty field (Ctrl-Shift-Enter)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(296, 6);
            // 
            // removeOrphanLinesToolStripMenuItem
            // 
            this.removeOrphanLinesToolStripMenuItem.Name = "removeOrphanLinesToolStripMenuItem";
            this.removeOrphanLinesToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.removeOrphanLinesToolStripMenuItem.Text = "Remove &orphan lines";
            this.removeOrphanLinesToolStripMenuItem.ToolTipText = "Remove any lines that don\'t exist in the reference language. Do this last, before" +
    " exporting the finished translation.";
            this.removeOrphanLinesToolStripMenuItem.Click += new System.EventHandler(this.removeOrphanLinesToolStripMenuItem_Click);
            // 
            // fillInTheBlanksToolStripMenuItem
            // 
            this.fillInTheBlanksToolStripMenuItem.Name = "fillInTheBlanksToolStripMenuItem";
            this.fillInTheBlanksToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.fillInTheBlanksToolStripMenuItem.Text = "Fill in the &blanks";
            this.fillInTheBlanksToolStripMenuItem.ToolTipText = "Copies the reference texts to the translated texts as is, if you don\'t want to tr" +
    "anslate them.";
            this.fillInTheBlanksToolStripMenuItem.Click += new System.EventHandler(this.fillInTheBlanksToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(296, 6);
            // 
            // suggestTextToolStripMenuItem
            // 
            this.suggestTextToolStripMenuItem.Name = "suggestTextToolStripMenuItem";
            this.suggestTextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.suggestTextToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.suggestTextToolStripMenuItem.Text = "Suggest text";
            this.suggestTextToolStripMenuItem.ToolTipText = "Suggests a text in the current or selected fields based on name and text lookup i" +
    "n the string suggestions you have loaded.";
            this.suggestTextToolStripMenuItem.Click += new System.EventHandler(this.suggestTextToolStripMenuItem_Click);
            // 
            // displaySuggestionInfoToolStripMenuItem
            // 
            this.displaySuggestionInfoToolStripMenuItem.Name = "displaySuggestionInfoToolStripMenuItem";
            this.displaySuggestionInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F12)));
            this.displaySuggestionInfoToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.displaySuggestionInfoToolStripMenuItem.Text = "Display suggestion info";
            this.displaySuggestionInfoToolStripMenuItem.Click += new System.EventHandler(this.displaySuggestionInfoToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 585);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "FastTranslate by Markus Kvist for nopCommerce 2.30 to 3.90+";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openReferenceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToTranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTranslationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFile3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFile4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeOrphanLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillInTheBlanksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNextemptyFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPreviousEmptyFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSuggestionReferenceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suggestTextToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeafResource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value4;
        private System.Windows.Forms.ToolStripMenuItem displaySuggestionInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem saveTranslationAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCurrentTextsToSuggestionToolStripMenuItem;
    }
}

