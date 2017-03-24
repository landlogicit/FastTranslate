using System.Collections.Generic;
using System.Linq;
using FastTranslate.Suggestions;

namespace FastTranslate.ResourceFiles
{
    public class ResourceFile
    {
        private readonly List<Resource> _resources = new List<Resource>();
        public string LanguageName { get; set; }

        public IList<Resource> Resources
        {
            get { return _resources.AsReadOnly(); }
        }

        public void Add(Resource item)
        {
            _resources.Add(item);
        }

        public Resource FindResourceByName(string name)
        {
            return _resources.FirstOrDefault(o => o.Name == name);
        }
    }
}