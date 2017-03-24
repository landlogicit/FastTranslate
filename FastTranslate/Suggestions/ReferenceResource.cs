using System;
using System.Linq;

namespace FastTranslate.Suggestions
{
    public class ReferenceResource
    {
        private readonly string[] _resourceNameParts;

        public ReferenceResource(TranslatedResource resource)
        {
            Resource = resource;
            _resourceNameParts = GetResourceParts(Resource.Name);
        }

        public TranslatedResource Resource { get; private set; }

        private bool ContainsResourcePart(string resourcePart)
        {
            return _resourceNameParts.Contains(resourcePart);
        }

        public int CountMatchingResourceParts(string resourceName)
        {
            return GetResourceParts(resourceName)
                .Count(ContainsResourcePart);
        }

        private string[] GetResourceParts(string resourceName)
        {
            return resourceName.Split('.');
        }

        public bool MatchesExactText(string name)
        {
            return string.Compare(name, Resource.Text, true) == 0;
        }

        public bool MatchesLeaf(string leaf)
        {
            return Resource.Name.Equals(leaf, StringComparison.InvariantCultureIgnoreCase)
                || Resource.Name.EndsWith("." + leaf, StringComparison.InvariantCultureIgnoreCase);
        }

        public int CountResourceParts()
        {
            return _resourceNameParts.Length;
        }
    }
}