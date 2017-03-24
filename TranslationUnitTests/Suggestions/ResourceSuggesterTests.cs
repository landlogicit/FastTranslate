using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastTranslate.Suggestions;
using FluentAssertions;
using NUnit.Framework;

namespace TranslationUnitTests.Suggestions
{
    [TestFixture]
    public class ResourceSuggesterTests
    {
        private const string SearchText = "search text";
        private const string AText = "a text";
        private const string AnotherText = "another text";
        private const string AnyTranslatedText = "any translated text";
        
        ResourceSuggester _suggester;

        [SetUp]
        public void Setup()
        {
            _suggester = new ResourceSuggester();
        }

        private TranslatedResource CreateTranslatedResource(string name, string text)
        {
            return new TranslatedResource(name, text, AnyTranslatedText);
        }

        private IList<ResourceSuggestion> AddResourcesAndGetSuggestions(IEnumerable<TranslatedResource> resources, TranslatedResource resource)
        {
            _suggester.AddResources(resources);

            var suggestions = _suggester.GetSuggestionsFor(resource);
            return suggestions;
        }

        [Test]
        public void AddResourceWithDuplicateName_ShouldReplaceExistingResourceWhenReplaceExistingResourcesIsTrue()
        {
            _suggester.ReplaceExistingResources = true;
            var resources = new[]
            {
                CreateTranslatedResource("A.B", SearchText),
                CreateTranslatedResource("A.B", AnotherText),
            };
            var expectedResult = resources[1];

            _suggester.AddResource(resources[0]);
            _suggester.AddResource(resources[1]);

            _suggester.ReferenceResources.Should().HaveCount(1);
            _suggester.ReferenceResources.Single().Resource.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void AddResourceWithDuplicateName_ShouldNotReplaceExistingResourceWhenReplaceExistingResourcesIsFalse()
        {
            _suggester.ReplaceExistingResources = false;
            var resources = new[]
            {
                CreateTranslatedResource("A.B", SearchText),
                CreateTranslatedResource("A.B", AnotherText),
            };
            var expectedResult = resources[0];

            _suggester.AddResource(resources[0]);
            _suggester.AddResource(resources[1]);

            _suggester.ReferenceResources.Should().HaveCount(1);
            _suggester.ReferenceResources.Single().Resource.ShouldBeEquivalentTo(expectedResult);
        }

        [Test]
        public void ExactTextMatch_ShouldReturnResultWithMostNumberOfMatchingNameElementsFirst()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.B", SearchText),
                CreateTranslatedResource("A.B.C", SearchText),
                CreateTranslatedResource("A.B.C.D", SearchText),
                CreateTranslatedResource("A.B.X.C", SearchText),
            };
            TranslatedResource searchResource = resources[1];
            var expectedResult = resources[0];

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Should().HaveCount(4);
            suggestions.Last().Resource.Should().BeSameAs(expectedResult);
        }

        [Test]
        public void ExactTextMatchAndSameNumberOfMatchingNameElements_ShouldPrioritizeLeastNumberOfElements()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.B.C.D", SearchText),
                CreateTranslatedResource("A.B.C", SearchText),
                CreateTranslatedResource("A.B.X.C", SearchText),
            };
            TranslatedResource searchResource = resources[1];
            var expectedResult = new[]
            {
                resources[1],
                resources[0],
                resources[2],
            };

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Select(o => o.Resource).Should().ContainInOrder(expectedResult);
        }

        [Test]
        public void NoExactTextMatchAndNoLeafMatch_ShouldReturnEmptyResult()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.B.C", AnotherText),
            };
            var searchResource = CreateTranslatedResource("A.B", SearchText);

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Should().BeEmpty();
        }

        [Test]
        public void NoExactTextMatch_ShouldOnlyReturnResourcesWithMatchingLeaf()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.B.C", AnotherText),
                CreateTranslatedResource("A.B.C.D", AnotherText),
                CreateTranslatedResource("A.B.X.C", AnotherText),
                CreateTranslatedResource("C", AnotherText),
            };
            var searchResource = CreateTranslatedResource("A.B.C", SearchText);
            var expectedResult = new[]
            {
                resources[0],
                resources[2],
                resources[3],
            };

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Select(o => o.Resource).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void NoExactTextMatch_ShouldOnlyReturnResourcesWhereMatchingLeafIsNotInInvalidLeafNames()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.B.C", AnotherText),
                CreateTranslatedResource("A.X.B.C", AnotherText),
                CreateTranslatedResource("A.B.X.C", AnotherText),
                CreateTranslatedResource("A.B.C.X", AnotherText),
            };
            var searchResource = CreateTranslatedResource("A.B.C", SearchText);
            var expectedResult = new[]
            {
                resources[0],
                resources[1],
            };

            var suggester = new ResourceSuggester();
            suggester.AddResources(resources);
            suggester.AddInvalidLeafNames(new [] {"C"});

            var suggestions = suggester.GetSuggestionsFor(searchResource);

            suggestions.Select(o => o.Resource).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void NoExactTextMatch_ShouldPrioritizeByTextSimilarity()
        {
            var resources = new[]
            {
                CreateTranslatedResource("A.X", "The fast yellow fox leaps over the lazy cat"), // 4 differences
                CreateTranslatedResource("B.X", "The quick brown fox leaps over the lazy cat"), // 2 differences
                CreateTranslatedResource("C.X", "The quick brown fox jumps over the lazy cat"), // 1 difference
                CreateTranslatedResource("D.X", "The quick fox"), // 6 differences
            };
            var searchResource = CreateTranslatedResource("Y.X", "The quick brown fox jumps over the lazy dog");
            var expectedResult = new[]
            {
                resources[2],
                resources[1],
                resources[0],
                resources[3],
            };

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Select(o => o.Resource).Should().ContainInOrder(expectedResult);
        }

        [Test]
        public void NoExactTextMatch_ShouldDiscardDissimilarResultsWhenMinimumTextSimilarityIsSet()
        {
            _suggester.MinimumTextSimilarity = 0.5;
            var resources = new[]
            {
                CreateTranslatedResource("C.X", "The quick brown fox jumps over the lazy cat"), // 1 difference
                CreateTranslatedResource("D.X", "The quick fox"), // 6 differences
            };
            var searchResource = CreateTranslatedResource("Y.X", "The quick brown fox jumps over the lazy dog");
            var expectedResult = new[]
            {
                resources[1],
            };

            var suggestions = AddResourcesAndGetSuggestions(resources, searchResource);

            suggestions.Select(o => o.Resource).Should().ContainInOrder(expectedResult);
        }
    }
}