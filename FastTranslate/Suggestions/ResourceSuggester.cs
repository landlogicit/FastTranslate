using System.Collections.Generic;
using System.Linq;

namespace FastTranslate.Suggestions
{
    public class ResourceSuggester
    {
        private readonly SortedSet<string> _invalidLeafNames = new SortedSet<string>();
        private readonly Levenstein _levenstein = new Levenstein();
        private readonly List<ReferenceResource> _referenceResources = new List<ReferenceResource>();

        public double MinimumTextSimilarity { get; set; }
        public bool ReplaceExistingResources { get; set; }

        public ResourceSuggester()
        {
            ReplaceExistingResources = true;
        }

        public IList<ReferenceResource> ReferenceResources
        {
            get { return _referenceResources.AsReadOnly(); }
        }

        public void AddResources(IEnumerable<TranslatedResource> resources)
        {
            foreach (TranslatedResource resource in resources)
            {
                AddResource(resource);
            }
        }

        public void AddResource(TranslatedResource resource)
        {
            if (string.IsNullOrEmpty(resource.Name) || string.IsNullOrEmpty(resource.Text))
                return;
            ReferenceResource existingReferenceResource =
                _referenceResources.FirstOrDefault(r => r.Resource.Name == resource.Name);
            if (existingReferenceResource != null && !ReplaceExistingResources)
                return;
            if (ReplaceExistingResources)
                _referenceResources.Remove(existingReferenceResource);
            _referenceResources.Add(new ReferenceResource(resource));
        }

        public void AddInvalidLeafNames(IEnumerable<string> invalidLeafNames)
        {
            foreach (string invalidLeafName in invalidLeafNames)
            {
                _invalidLeafNames.Add(invalidLeafName);
            }
        }

        public ResourceSuggestion GetSuggestionFor(Resource resource)
        {
            return GetSuggestionsFor(resource).FirstOrDefault();
        }

        public IList<ResourceSuggestion> GetSuggestionsFor(Resource resource)
        {
            if (string.IsNullOrEmpty(resource.Name))
                return new List<ResourceSuggestion>();

            List<ResourceSuggestion> suggestions = _referenceResources
                .Select(referenceResource => CreateSuggestion(resource, referenceResource))
                .Where(SuggestionShouldBeIncludedInResult)
                .OrderByDescending(o => o.IsExactTextMatch)
                .ThenByDescending(o => o.NumberOfMatchingResourceParts)
                .ThenBy(o => o.NumberOfResourceParts)
                .ToList();

            if (!suggestions.Any())
                return suggestions;

            if (suggestions.First().IsExactTextMatch)
                return suggestions;

            foreach (ResourceSuggestion resourceSuggestion in suggestions)
            {
                resourceSuggestion.TextSimilarity = CalculateTextSimilarity(resourceSuggestion.Resource.Text,
                    resource.Text);
            }
            return suggestions
                .OrderByDescending(o => o.TextSimilarity)
                .ToList();
        }

        private double CalculateTextSimilarity(string s1, string s2)
        {
            return _levenstein.GetSimilarity(s1, s2);
        }

        private static bool SuggestionShouldBeIncludedInResult(ResourceSuggestion resourceSuggestion)
        {
            return resourceSuggestion.IsExactTextMatch || resourceSuggestion.MatchesLeaf;
        }

        private ResourceSuggestion CreateSuggestion(Resource resource, ReferenceResource referenceResource)
        {
            var resourceSuggestion = new ResourceSuggestion
            {
                Resource = referenceResource.Resource,
                NumberOfResourceParts = referenceResource.CountResourceParts(),
                NumberOfMatchingResourceParts = referenceResource.CountMatchingResourceParts(resource.Name),
                IsExactTextMatch = referenceResource.MatchesExactText(resource.Text),
                MatchesLeaf = referenceResource.MatchesLeaf(GetLeaf(resource.Name)),
            };
            return resourceSuggestion;
        }

        private string GetLeaf(string resourceName)
        {
            string[] resourceNameSplit = resourceName.Split('.');
            string leaf = resourceNameSplit.Last();
            if (_invalidLeafNames.Contains(leaf) && resourceNameSplit.Length > 1)
                leaf = string.Format("{0}.{1}", resourceNameSplit[resourceNameSplit.Length - 2], leaf);
            return leaf;
        }
    }

    //public string SuggestTextFor(string resourceName, string resourceText)
    //{
    //    return null;
    //}
}