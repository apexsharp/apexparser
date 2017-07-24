using Apex.Schema;

namespace Apex.System
{
    public class Schema
    {
        public static List<DescribeDataCategoryGroupStructureResult> DescribeDataCategoryGroupStructures(
            List<DataCategoryGroupSobjectTypePair> pairs, bool topCategoriesOnly)
        {
            throw new global::System.NotImplementedException("Schema.DescribeDataCategoryGroupStructures");
        }

        public static List<DescribeDataCategoryGroupResult> DescribeDataCategoryGroups(List<string> sobjects)
        {
            throw new global::System.NotImplementedException("Schema.DescribeDataCategoryGroups");
        }

        public static List<DescribeSObjectResult> DescribeSObjects(List<string> types)
        {
            throw new global::System.NotImplementedException("Schema.DescribeSObjects");
        }

        public static List<DescribeTabSetResult> DescribeTabs()
        {
            throw new global::System.NotImplementedException("Schema.DescribeTabs");
        }

        public static Map<String, SObjectType> GetAppDescribe(string appName)
        {
            throw new global::System.NotImplementedException("Schema.GetAppDescribe");
        }

        public static Map<String, SObjectType> GetGlobalDescribe()
        {
            throw new global::System.NotImplementedException("Schema.GetGlobalDescribe");
        }

        public static Map<String, SObjectType> GetModuleDescribe()
        {
            throw new global::System.NotImplementedException("Schema.GetModuleDescribe");
        }

        public static Map<String, SObjectType> GetModuleDescribe(string moduleName)
        {
            throw new global::System.NotImplementedException("Schema.GetModuleDescribe");
        }
    }
}