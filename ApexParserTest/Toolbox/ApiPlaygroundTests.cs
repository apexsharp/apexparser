using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ApexParserTest.Toolbox
{
    [TestFixture]
    public class ApiPlaygroundTests
    {
        public class DataProviderParameter
        {
            public string Name { get; }
            public object Value { get; }
            public DataProviderParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }
        }

        public class DataProviderCommand
        {
            public DataProviderCommand(string query) { }
            public List<DataProviderParameter> Parameters { get; } = new List<DataProviderParameter>();
            public void Execute()
            {
                Console.WriteLine("Executing command with parameters: ");
                foreach (var p in Parameters)
                {
                    Console.WriteLine("{0} => {1}", p.Name, p.Value);
                }
            }
        }

        public class Soql
        {
            public static void Query(string soql, params object[] arguments)
            {
                // replace parameter names — :email with p0, :name with p1, etc.
                var index = 0;
                var soqlQuery = Regex.Replace(soql, @"(\:\S+)", m => $"p{index++}");
                var command = new DataProviderCommand(soqlQuery);

                // prepare parameters for the data provider command
                for (var i = 0; i < arguments.Length; i++)
                {
                    var param = new DataProviderParameter("p" + i, arguments[0]);
                    command.Parameters.Add(param);
                }

                // execute the command and get the results
                command.Execute();
            }

            public static PolymorphicQueryResult<T> Query<T>(string soql, params object[] arguments) where T : new()
            {
                return new PolymorphicQueryResult<T>(soql, arguments);
            }
        }

        [Test]
        public void TestSoqlApiExample()
        {
            Soql.Query("select id from Customer where email = :email", "jay@jayonsoftware.com");
            Soql.Query("select id from Customer where email = :email and name = :name", "jay@jayonsoftware.com", "jay");
            Soql.Query("select id from Customer where email = :email and name = :name and age > :age", "jay@jayonsoftware.com", "jay", 20);
        }

        public class PolymorphicQueryResult<T> where T : new()
        {
            public PolymorphicQueryResult(string query, params object[] parameters)
            {
                Parameters = parameters;
                OriginalQuery = query;
                PreparedQuery = query; // replace query parameters with the values

                // simulate the query execution
                // real query should be executed lazily
                QueryResult = new List<T> { new T(), new T(), new T() };
            }

            public object[] Parameters { get; }

            public string OriginalQuery { get; }

            public string PreparedQuery { get; }

            public List<T> QueryResult { get; }

            public static implicit operator string(PolymorphicQueryResult<T> query) => query.OriginalQuery;

            public static implicit operator List<T>(PolymorphicQueryResult<T> query) => query.QueryResult;

            public static implicit operator T(PolymorphicQueryResult<T> query) => query.QueryResult.FirstOrDefault();
        }

        public class Customer
        {
            public string Name => "Jay";
            public string Email => "jay@jayonsoftware.com";
        }

        [Test]
        public void TestPolymorphicQueryApi()
        {
            // multiple results
            List<Customer> list = Soql.Query<Customer>("select * from Customer where email = :email and name = :name and age > :age", "jay@jayonsoftware.com", "jay", 20);
            Assert.NotNull(list);
            Assert.AreEqual(3, list.Count);

            // single result
            Customer customer = Soql.Query<Customer>("select * from Customer where email = :email and name = :name and age > :age", "jay@jayonsoftware.com", "jay", 20);
            Assert.NotNull(customer);
            Assert.AreEqual("Jay", customer.Name);
            Assert.AreEqual("jay@jayonsoftware.com", customer.Email);

            // query text
            string query = Soql.Query<Customer>("select * from Customer where email = :email and name = :name and age > :age", "jay@jayonsoftware.com", "jay", 20);
            Assert.NotNull(query);
            Assert.AreEqual("select * from Customer where email = :email and name = :name and age > :age", query);
        }
    }
}
