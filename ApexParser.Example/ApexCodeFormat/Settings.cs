namespace ApexSharpDemo.ApexCodeFormat
{
    public class Settings
    {
        // The { and } brackets will start on a new line except for {get;set;}
        public bool OpenCloseBracesInNewLine { get; set; } = true;

        // Code will be on a single line until the end of ';'
        public bool SingleLine { get; set; } = true;

        // What should be in the indent
        public int TabIndentSize { get; set; } = 4;

        // Annotation TestMethod is being depreciated by @isTest, thus replace it
        public bool ReplaceTestMethod { get; set; } = true;

        /*
         * SOQL Formatting
         * Each statement should be on its own line
         *
         * SELECT Id, Name
         * FROM Account
         * WHERE Name = 'Sandy'
         */
        public bool SoqlFormat { get; set; } = true;

        // Other items by default.
        // Comments between statements will show up in the top of the statements.
    }
}
