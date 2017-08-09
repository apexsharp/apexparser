using Apex.System;
using ApexClasses;

namespace Apex.UserProvisioning
{
    public class ProvisioningBatchable
    {
        public ProvisioningBatchable(List<SObject> newRows)
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.Clone");
        }

        //public void Execute(Database.BatchableContext BC,List<UserProvisioningRequest> scope){throw new global::System.NotImplementedException("ProvisioningBatchable.Execute");}
        public void Finish(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.Finish");
        }

        public Map<String, object> FlowInputPreprocessing(Map<String, object> myMap)
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.FlowInputPreprocessing");
        }

        public void FlowPostProcessing(UserProvisioning.ProvisioningProcessHandlerOutput provOutput, SObject thisUPR)
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.FlowPostProcessing");
        }

        public string GetEventPrefix()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.GetEventPrefix");
        }

        public string GetFlowName()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.GetFlowName");
        }

        public string GetFlowNamespace()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.GetFlowNamespace");
        }

        public List<SObject> GetPerBatchUPL()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.GetPerBatchUPL");
        }

        //public List<UserProvisioningRequest> GetPerBatchUPR(){throw new global::System.NotImplementedException("ProvisioningBatchable.GetPerBatchUPR");}
        public Map<Id, SObject> GetUprToNewUplMap()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.GetUprToNewUplMap");
        }

        public bool HasFlow()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.HasFlow");
        }

        public bool HasFlowOrApex()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.HasFlowOrApex");
        }

        public void PostBatchProcessing()
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.PostBatchProcessing");
        }

        public Database.QueryLocator Start(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("ProvisioningBatchable.Start");
        }
    }
}