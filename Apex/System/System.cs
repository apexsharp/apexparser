using SalesForceAPI.Apex;

namespace Apex.System
{
    public class System
    {
        public static void AbortJob(string jobId)
        {
            throw new global::System.NotImplementedException("System.AbortJob");
        }

        public static void Assert(bool condition)
        {
            throw new global::System.NotImplementedException("System.Assert : Use NUnit Asserts");
        }

        public static void Assert(bool condition, object msg)
        {
            throw new global::System.NotImplementedException("System.Assert : Use NUnit Asserts");
        }

        public static void AssertEquals(object expected, object actual)
        {
            throw new global::System.NotImplementedException("System.AssertEquals : Use NUnit Asserts");
        }

        public static void AssertEquals(object expected, object actual, object msg)
        {
            throw new global::System.NotImplementedException("System.AssertEquals : Use NUnit Asserts");
        }

        public static void AssertNotEquals(object expected, object actual)
        {
            throw new global::System.NotImplementedException("System.AssertNotEquals : Use NUnit Asserts");
        }

        public static void AssertNotEquals(object expected, object actual, object msg)
        {
            throw new global::System.NotImplementedException("System.AssertNotEquals : Use NUnit Asserts");
        }

        public static void ChangeProtection(string apiName, string typeApiName, string protection)
        {
            throw new global::System.NotImplementedException("System.ChangeProtection");
        }

        public static bool CheckPackageBooleanValue(string apiName)
        {
            throw new global::System.NotImplementedException("System.CheckPackageBooleanValue");
        }

        public static Date CheckPackageDateValue(string apiName)
        {
            throw new global::System.NotImplementedException("System.CheckPackageDateValue");
        }

        public static int CheckPackageIntegerValue(string apiName)
        {
            throw new global::System.NotImplementedException("System.CheckPackageIntegerValue");
        }

        public static bool CheckPermission(string apiName)
        {
            throw new global::System.NotImplementedException("System.CheckPermission");
        }

        public static PageReference CurrentPageReference()
        {
            throw new global::System.NotImplementedException("System.CurrentPageReference");
        }

        public static long CurrentTimeMillis()
        {
            throw new global::System.NotImplementedException("System.CurrentTimeMillis");
        }

        public static void Debug(object o)
        {
            global::System.Console.WriteLine(o);
        }

        public static void Debug(object logLevel, object o)
        {
            global::System.Console.WriteLine(o);
        }

        public static Id EnqueueJob(object queueable)
        {
            throw new global::System.NotImplementedException("System.EnqueueJob");
        }

        public static bool Equals(object left, object right)
        {
            throw new global::System.NotImplementedException("System.Equals");
        }

        //public static ApplicationReadWriteMode GetApplicationReadWriteMode(){throw new global::System.NotImplementedException("System.GetApplicationReadWriteMode");}
        public static int HashCode(object obj)
        {
            throw new global::System.NotImplementedException("System.HashCode");
        }

        public static bool IsBatch()
        {
            throw new global::System.NotImplementedException("System.IsBatch");
        }

        public static bool IsFuture()
        {
            throw new global::System.NotImplementedException("System.IsFuture");
        }

        public static bool IsQueueable()
        {
            throw new global::System.NotImplementedException("System.IsQueueable");
        }

        public static bool IsScheduled()
        {
            throw new global::System.NotImplementedException("System.IsScheduled");
        }

        public static void MovePassword(Id targetUserId, Id sourceUserId)
        {
            throw new global::System.NotImplementedException("System.MovePassword");
        }

        public static DateTime Now()
        {
            throw new global::System.NotImplementedException("System.Now");
        }

        public static List<Id> Process(List<Id> workitemIds, string action, string commments, string nextApprover)
        {
            throw new global::System.NotImplementedException("System.Process");
        }

        public static int PurgeOldAsyncJobs(Date date)
        {
            throw new global::System.NotImplementedException("System.PurgeOldAsyncJobs");
        }

        public static Version RequestVersion()
        {
            throw new global::System.NotImplementedException("System.RequestVersion");
        }

        public static ResetPasswordResult ResetPassword(Id userId, bool sendUserEmail)
        {
            throw new global::System.NotImplementedException("System.ResetPassword");
        }

        public static void RunAs(Package.Version version)
        {
            throw new global::System.NotImplementedException("System.RunAs");
        }

        public static void RunAs(SObject user)
        {
            throw new global::System.NotImplementedException("System.RunAs");
        }

        public static void RunAs(SObject user, object block)
        {
            throw new global::System.NotImplementedException("System.RunAs");
        }

        public static string Schedule(string jobName, string cronExp, object schedulable)
        {
            throw new global::System.NotImplementedException("System.Schedule");
        }

        public static string ScheduleBatch(object batchable, string jobName, int minutesFromNow)
        {
            throw new global::System.NotImplementedException("System.ScheduleBatch");
        }

        public static string ScheduleBatch(object batchable, string jobName, int minutesFromNow, int scopeSize)
        {
            throw new global::System.NotImplementedException("System.ScheduleBatch");
        }

        public static void SetPackageBooleanValue(string apiName, bool value)
        {
            throw new global::System.NotImplementedException("System.SetPackageBooleanValue");
        }

        public static void SetPackageDateValue(string apiName, Date value)
        {
            throw new global::System.NotImplementedException("System.SetPackageDateValue");
        }

        public static void SetPackageIntegerValue(string apiName, int value)
        {
            throw new global::System.NotImplementedException("System.SetPackageIntegerValue");
        }

        public static void SetPassword(Id userId, string password)
        {
            throw new global::System.NotImplementedException("System.SetPassword");
        }

        public static List<Id> Submit(List<Id> ids, string commments, string nextApprover)
        {
            throw new global::System.NotImplementedException("System.Submit");
        }

        public static Date Today()
        {
            throw new global::System.NotImplementedException("System.Today");
        }
    }
}