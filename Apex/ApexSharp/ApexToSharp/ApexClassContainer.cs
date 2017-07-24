using System.Collections.Generic;
using System.IO;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexClassContainer
    {
        public List<ApexList> ApexListList = new List<ApexList>();
        public List<ApexTocken> ApexTokens = new List<ApexTocken>();


        public List<string> CodeComments = new List<string>();

        public ApexClassContainer(FileInfo apexSource)
        {
            ApexSource = apexSource;
            ClassName = ApexSource.Name;
        }

        public FileInfo ApexSource { get; set; }
        private string ClassName { get; set; }

        public string GetClassName()
        {
            var className = ClassName.Replace(".cls", "");
            return className;
        }
    }
}