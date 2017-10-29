using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class Limits
    {
        private readonly ApexSharpConfig _connectionDetail;

        public Limits(ApexSharpConfig connectionDetail)
        {
            _connectionDetail = connectionDetail;
        }

        public int GetDailyApiLimit(LimitType limitType)
        {
            switch (limitType)
            {
                case LimitType.DailyApiRequests:
                    return GetLimits().DailyApiRequests.Max;
            }
            return 0;
        }

        public int GetRemainingApiLimit(LimitType limitType)
        {
            switch (limitType)
            {
                case LimitType.DailyApiRequests:
                    return GetLimits().DailyApiRequests.Remaining;
            }
            return 0;
        }


        public SalesForceApiLimits GetLimits()
        {
            Db db = new Db(_connectionDetail);
            var limitWait = db.GetApiLimits();

            limitWait.Wait();

            return limitWait.Result;
        }
    }
}