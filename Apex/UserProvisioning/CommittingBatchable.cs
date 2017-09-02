using Apex.System;
using SalesForceAPI.Apex;

namespace Apex.UserProvisioning
{
    public class CommittingBatchable
    {
        public CommittingBatchable(string uprId)
        {
            throw new global::System.NotImplementedException("CommittingBatchable");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("CommittingBatchable.Clone");
        }

        public void Execute(Database.BatchableContext BC, List<SObject> scope)
        {
            throw new global::System.NotImplementedException("CommittingBatchable.Execute");
        }

        public void Finish(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("CommittingBatchable.Finish");
        }

        public Database.QueryLocator Start(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("CommittingBatchable.Start");
        }
    }
}