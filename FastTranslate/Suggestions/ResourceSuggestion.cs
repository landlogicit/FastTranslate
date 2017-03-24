namespace FastTranslate.Suggestions
{
    public class ResourceSuggestion
    {
        public static ResourceSuggestion Empty = new ResourceSuggestion();

        public TranslatedResource Resource { get; set; }
        public bool IsExactTextMatch { get; set; }
        public bool MatchesLeaf { get; set; }
        public int NumberOfResourceParts { get; set; }
        public int NumberOfMatchingResourceParts { get; set; }
        public double? TextSimilarity { get; set; }

        public override string ToString()
        {
            return string.Format("ExactMatch={0}, MatchesLeaf={1}, MatchingParts={3}/{2}, Similarity={4}, [{5}]",
                IsExactTextMatch, MatchesLeaf, NumberOfResourceParts, NumberOfMatchingResourceParts, TextSimilarity, Resource);
        }
    }
}