using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;

namespace SalesForceAPI
{
    public class SoqlQuery<T> : IEnumerable<T>
    {
        public SoqlQuery(Lazy<List<T>> lazyResult, string originalQuery, string preparedQuery = null, params object[] parameters)
        {
            QueryResult = lazyResult;
            OriginalQuery = originalQuery;
            PreparedQuery = preparedQuery ?? originalQuery;
            Parameters = parameters;
            Fields = GenericExpressionHelper.GetSoqlFields(originalQuery);
        }

        public string OriginalQuery { get; }

        public string PreparedQuery { get; }

        public object[] Parameters { get; }

        public string[] Fields { get; }

        public Lazy<List<T>> QueryResult { get; }

        public IEnumerator<T> GetEnumerator() => QueryResult.Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => QueryResult.Value.GetEnumerator();

        public static implicit operator string(SoqlQuery<T> query) => query.OriginalQuery;

        public static implicit operator List<T>(SoqlQuery<T> query) => query.QueryResult.Value;

        public static implicit operator T[](SoqlQuery<T> query) => query.QueryResult.Value.ToArray();

        public static implicit operator T(SoqlQuery<T> query) => query.QueryResult.Value.FirstOrDefault();
    }
}
