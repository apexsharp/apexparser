using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class Limits
    {
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
            Db db = new Db();
            var limitWait = db.GetApiLimits();

            limitWait.Wait();

            return limitWait.Result;
        }
    }
}