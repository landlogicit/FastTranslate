using FastTranslate.Suggestions;
using FluentAssertions;
using NUnit.Framework;

namespace TranslationUnitTests.Suggestions
{
    [TestFixture]
    public class ReferenceResourceTests
    {
        private const string ResourceName = "A.B.C.D";
        private const string AnySearchText = "any search text";
        private const string AnyTranslatedText = "any translated text";
        private TranslatedResource TestResource = new TranslatedResource(ResourceName, AnySearchText, AnySearchText);

        [Test]
        public void EachPartShouldBeCountedOnce()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountMatchingResourceParts("A").Should().Be(1);
            referenceResource.CountMatchingResourceParts("B").Should().Be(1);
            referenceResource.CountMatchingResourceParts("C").Should().Be(1);
            referenceResource.CountMatchingResourceParts("D").Should().Be(1);
        }

        [Test]
        public void SameString_ShouldHaveResultWithSameNumberOfParts()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountMatchingResourceParts(ResourceName).Should().Be(4);
        }

        [Test]
        public void PartString_ShouldHaveResultWithSameNumberOfParts()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountMatchingResourceParts("A.B").Should().Be(2);
        }

        [Test]
        public void SeparateParts_ShouldHaveResultWithMatchingNumberOfParts()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountMatchingResourceParts("A.X.B.Y").Should().Be(2);
        }

        [Test]
        public void SeparatePartsInDifferentOrder_ShouldHaveResultWithMatchingNumberOfParts()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountMatchingResourceParts("B.X.A.Y").Should().Be(2);
        }

        [Test]
        public void MatchesExactText_ShouldReturnTrueWhenTextIsEqualToResourceText()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.MatchesExactText(TestResource.Text).Should().BeTrue();
        }

        [Test]
        public void MatchesExactText_ShouldReturnFalseWhenTextIsNotEqualToResourceText()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.MatchesExactText("another text").Should().BeFalse();
        }

        [Test]
        public void MatchesLeaf_ShouldReturnTrueWhenEntireLeafMatchesText()
        {
            var referenceResource = new ReferenceResource(new TranslatedResource("AB.CD.EF", AnySearchText, AnyTranslatedText));

            referenceResource.MatchesLeaf("ef").Should().BeTrue();
            referenceResource.MatchesLeaf("CD.ef").Should().BeTrue();
        }

        [Test]
        public void MatchesLeaf_ShouldReturnFalseWhenPartLeafMatchesText()
        {
            var referenceResource = new ReferenceResource(new TranslatedResource("AB.CD.EF", AnySearchText, AnyTranslatedText));

            referenceResource.MatchesLeaf("F").Should().BeFalse();
            referenceResource.MatchesLeaf("D.EF").Should().BeFalse();
        }

        [Test]
        public void CountResourceParts_ShouldReturnNumberOfResourceParts()
        {
            var referenceResource = new ReferenceResource(TestResource);

            referenceResource.CountResourceParts().Should().Be(4);
        }
    }
}