using System.IO;
using System.Xml;
using System.Xml.Linq;
using FastTranslate.Suggestions;

namespace FastTranslate.ResourceFiles
{
    public class ResourceFileReader
    {
        private ResourceFile _resourceFile;

        public ResourceFile ReadXmlFile(string filename)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                return Read(reader);
            }
        }

        public ResourceFile ReadXml(string xml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                return Read(reader);
            }
        }

        private ResourceFile Read(XmlReader reader)
        {
            _resourceFile = new ResourceFile();
            reader.MoveToContent();
            var languageElement = XNode.ReadFrom(reader) as XElement;
            if (languageElement.Attribute("Name") != null)
                _resourceFile.LanguageName = languageElement.Attribute("Name").Value;
            ReadLocaleResourceElementsRecursive(languageElement, string.Empty);
            return _resourceFile;
        }

        /// <summary>
        /// Reads a LocaleResource element, both old version with fully qualified names,
        /// and new version with Children sub elements.
        /// </summary>
        /// <param name="node">The LocaleResource node</param>
        /// <param name="currentPath">The path, if any, of the parent element.</param>
        private void ReadLocaleResourceElementsRecursive(XElement node, string currentPath)
        {
            foreach (XElement localeResource in node.Elements("LocaleResource"))
            {
                string qualifiedName = AppendNameToPath(currentPath, localeResource.Attribute("Name").Value);
                XElement valueElement = localeResource.Element("Value");
                if (valueElement != null)
                    _resourceFile.Add(new Resource(qualifiedName, valueElement.Value.Trim()));
                XElement children = localeResource.Element("Children");
                if (children != null)
                    ReadLocaleResourceElementsRecursive(children, qualifiedName);
            }
        }

        private static string AppendNameToPath(string currentPath, string name)
        {
            return !string.IsNullOrEmpty(currentPath)
                ? string.Format("{0}.{1}", currentPath, name)
                : name;
        }
    }
}