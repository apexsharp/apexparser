using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SalesForceAPI.Attribute;

namespace SalesForceAPI
{
    public class SoqlCreator
    {
        // ToDo : Remove All this duplicate code
        public string GetSalesForceObjectName<T>()
        {
            string objectName = typeof(T).Name;
            ObjectNameAttribute objectNameAttrbute = typeof(T).GetCustomAttributes(typeof(ObjectNameAttribute), true)
                .FirstOrDefault() as ObjectNameAttribute;
            if (objectNameAttrbute != null)
            {
                objectName = objectNameAttrbute.SalesForceObjectName;
            }
            return objectName;
        }


        public string GetSoql<T>()
        {
            List<string> soqlList = Reflection<T>();


            // Build the SOQL
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");


            string newSoql = string.Join(",", soqlList);

            sb.Append(newSoql);

            sb.Append(" FROM ").Append(GetSalesForceObjectName<T>());
            return sb.ToString();
        }

        private List<string> Reflection<T>()
        {
            List<string> dbFieldInfoList = new List<string>();

            foreach (PropertyInfo membner in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                dbFieldInfoList.Add(membner.Name);
            }
            return dbFieldInfoList;
        }
    }
}