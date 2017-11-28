namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    [WithSharing]
    public class Label
    {
        public static void LabelDemo()
        {
            string demoLabel = System.Label.Demo;
            System.debug(demoLabel);
        }
    }
}
