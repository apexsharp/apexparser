using System.Text.RegularExpressions;

namespace SalesForceAPI
{
    public class UtilSalesForce
    {
        // Check to see if the value passed is a Salesforce Id        
        public static bool IsSalesforceId(string id)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9]{18}");
            var match = regex.Match(id);
            return match.Success;
        }
    }
}