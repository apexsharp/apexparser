using Apex.System;
using SalesForceAPI.ApexApi;

namespace Apex.UserProvisioning
{
    public class CollectingBatchable
    {
        public CollectingBatchable(string reconOffset, string uprId, string connectedAppId)
        {
            throw new global::System.NotImplementedException("CollectingBatchable");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.Clone");
        }

        //public void Execute(Database.BatchableContext BC,List<UserProvisioningRequest> scope){throw new global::System.NotImplementedException("CollectingBatchable.Execute");}
        public void Finish(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("CollectingBatchable.Finish");
        }

        public Map<String, object> FlowInputPreprocessing(Map<String, object> myMap)
        {
            throw new global::System.NotImplementedException("CollectingBatchable.FlowInputPreprocessing");
        }

        public void FlowPostProcessing(UserProvisioning.ProvisioningProcessHandlerOutput provOutput, SObject thisUPR)
        {
            throw new global::System.NotImplementedException("CollectingBatchable.FlowPostProcessing");
        }

        public string GetEventPrefix()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.GetEventPrefix");
        }

        public string GetFlowName()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.GetFlowName");
        }

        public string GetFlowNamespace()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.GetFlowNamespace");
        }

        public List<SObject> GetPerBatchUPL()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.GetPerBatchUPL");
        }

        //public List<UserProvisioningRequest> GetPerBatchUPR(){throw new global::System.NotImplementedException("CollectingBatchable.GetPerBatchUPR");}
        public Map<ID, SObject> GetUprToNewUplMap()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.GetUprToNewUplMap");
        }

        public bool HasFlow()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.HasFlow");
        }

        public bool HasFlowOrApex()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.HasFlowOrApex");
        }

        public void PostBatchProcessing()
        {
            throw new global::System.NotImplementedException("CollectingBatchable.PostBatchProcessing");
        }

        public Database.QueryLocator Start(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("CollectingBatchable.Start");
        }
    }
}