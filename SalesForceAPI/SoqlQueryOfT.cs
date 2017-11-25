using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForceAPI
{
    public class SoqlQuery<T>
    {
        public SoqlQuery(Lazy<List<T>> lazyResult, string originalQuery, string preparedQuery = null, params object[] parameters)
        {
            QueryResult = lazyResult;
            OriginalQuery = originalQuery;
            PreparedQuery = preparedQuery ?? originalQuery;
            Parameters = parameters;
        }

        public string OriginalQuery { get; }

        public string PreparedQuery { get; }

        public object[] Parameters { get; }

        public Lazy<List<T>> QueryResult { get; }

        public static implicit operator string(SoqlQuery<T> query) => query.OriginalQuery;

        public static implicit operator List<T>(SoqlQuery<T> query) => query.QueryResult.Value;

        public static implicit operator T(SoqlQuery<T> query) => query.QueryResult.Value.FirstOrDefault();
    }
}
