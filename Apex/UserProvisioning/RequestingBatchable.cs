using Apex.System;
using SalesForceAPI.ApexApi;

namespace Apex.UserProvisioning
{
    public class RequestingBatchable
    {
        public RequestingBatchable(List<SObject> newRows)
        {
            throw new global::System.NotImplementedException("RequestingBatchable");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("RequestingBatchable.Clone");
        }

        //public void Execute(Database.BatchableContext BC,List<UserProvisioningRequest> scope){throw new global::System.NotImplementedException("RequestingBatchable.Execute");}
        public void Finish(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("RequestingBatchable.Finish");
        }

        public Database.QueryLocator Start(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("RequestingBatchable.Start");
        }
    }
}