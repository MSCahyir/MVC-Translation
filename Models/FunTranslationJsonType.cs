namespace Translation.Models
{
    public class FunTranslationJsonType
    {
        public Success success { get; set; }
        public Contents contents { get; set; }

        public class Contents
        {
            public string translated { get; set; }
            public string text { get; set; }
            public Translation translation { get; set; }
        }

        public class Success
        {
            public int total { get; set; }
        }

        public class Translation
        {
            public string source { get; set; }
            public string destination { get; set; }
        }

    }
}