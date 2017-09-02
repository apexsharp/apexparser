using Apex.Search;
using SalesForceAPI.Apex;

namespace Apex.System
{
    public class Search
    {
        public static SearchResults Find(string searchQuery)
        {
            throw new global::System.NotImplementedException("Search.Find");
        }

        public static List<List<SObject>> Query(string searchQuery)
        {
            throw new global::System.NotImplementedException("Search.Query");
        }

        public static SuggestionResults Suggest(string searchQuery, string sObjectType, object options)
        {
            throw new global::System.NotImplementedException("Search.Suggest");
        }
    }
}