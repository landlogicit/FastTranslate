namespace FastTranslate.Suggestions
{
    public class Resource
    {
        public Resource(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public string Name { get; private set; }
        public string Text { get; private set; }

        public override string ToString()
        {
            return string.Format(@"Name=""{0}"", Text=""{1}""",
                Name, Text);
        }
    }

    public class TranslatedResource : Resource
    {
        public TranslatedResource(string name, string text, string translatedText)
            : base(name, text)
        {
            TranslatedText = translatedText;
        }

        public string TranslatedText { get; private set; }

        public override string ToString()
        {
            return string.Format(@"{0}, TranslatedText=""{1}""",
                base.ToString(), TranslatedText);
        }
    }
}