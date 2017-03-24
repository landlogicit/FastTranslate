using FastTranslate.ResourceFiles;
using FastTranslate.Suggestions;
using FluentAssertions;
using NUnit.Framework;

namespace TranslationUnitTests.ResourceFiles
{
    [TestFixture]
    public class ResourceFileReaderTests
    {
        [SetUp]
        public void Setup()
        {
            _reader = new ResourceFileReader();
        }

        private ResourceFileReader _reader;

        [Test]
        public void ShouldReadLanguageName()
        {
            const string xml = @"
<Language Name='English'>
</Language>
";
            ResourceFile resourceFile = _reader.ReadXml(xml);

            resourceFile.Should().NotBeNull();
            resourceFile.LanguageName.Should().Be("English");
        }

        [Test]
        public void ShouldReadSimpleResources()
        {
            const string xml = @"
<Language>
    <LocaleResource Name='AboutUs'>
        <Value>About us</Value>
    </LocaleResource>
    <LocaleResource Name='Account.AccountActivation'>
        <Value>Account activation</Value>
    </LocaleResource>
</Language>
";
            ResourceFile resourceFile = _reader.ReadXml(xml);

            resourceFile.Should().NotBeNull();
            resourceFile.Resources.Should().HaveCount(2);
            resourceFile.Resources[0].ShouldBeEquivalentTo(new Resource("AboutUs", "About us"));
            resourceFile.Resources[1].ShouldBeEquivalentTo(new Resource("Account.AccountActivation",
                "Account activation"));
        }

        [Test]
        public void ShouldReadNestedResources()
        {
            const string xml = @"
<Language>
	<LocaleResource Name='ExampleOfNestedResources'>
		<Value>Value of ExampleOfNestedResources</Value>
		<Children>
			<LocaleResource Name='Child1'>
				<Value>Value of Child1</Value>
			</LocaleResource>
			<LocaleResource Name='Child2'>
				<Value>Value of Child2</Value>
				<Children>
					<LocaleResource Name='Child3'>
				        <Value>Value of Child3</Value>
					</LocaleResource>
				</Children>
			</LocaleResource>
		</Children>
	</LocaleResource>
</Language>
";
            ResourceFile resourceFile = _reader.ReadXml(xml);

            resourceFile.Should().NotBeNull();
            resourceFile.Resources.Should().HaveCount(4);
            resourceFile.Resources[0].ShouldBeEquivalentTo(new Resource("ExampleOfNestedResources",
                "Value of ExampleOfNestedResources"));
            resourceFile.Resources[1].ShouldBeEquivalentTo(new Resource("ExampleOfNestedResources.Child1",
                "Value of Child1"));
            resourceFile.Resources[2].ShouldBeEquivalentTo(new Resource("ExampleOfNestedResources.Child2",
                "Value of Child2"));
            resourceFile.Resources[3].ShouldBeEquivalentTo(new Resource("ExampleOfNestedResources.Child2.Child3",
                "Value of Child3"));
        }
    }
}