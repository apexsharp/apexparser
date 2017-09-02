using Apex.System;
using SalesForceAPI.Apex;

namespace Apex.UserProvisioning
{
    public class PluginBatchable
    {
        public PluginBatchable(List<SObject> newRows)
        {
            throw new global::System.NotImplementedException("PluginBatchable");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("PluginBatchable.Clone");
        }

        //public void Execute(Database.BatchableContext BC,List<UserProvisioningRequest> scope){throw new global::System.NotImplementedException("PluginBatchable.Execute");}
        public Map<String, object> FlowInputPreprocessing(Map<String, object> param1)
        {
            throw new global::System.NotImplementedException("PluginBatchable.FlowInputPreprocessing");
        }

        public void FlowPostProcessing(UserProvisioning.ProvisioningProcessHandlerOutput param1, SObject param2)
        {
            throw new global::System.NotImplementedException("PluginBatchable.FlowPostProcessing");
        }

        public string GetEventPrefix()
        {
            throw new global::System.NotImplementedException("PluginBatchable.GetEventPrefix");
        }

        public string GetFlowName()
        {
            throw new global::System.NotImplementedException("PluginBatchable.GetFlowName");
        }

        public string GetFlowNamespace()
        {
            throw new global::System.NotImplementedException("PluginBatchable.GetFlowNamespace");
        }

        public List<SObject> GetPerBatchUPL()
        {
            throw new global::System.NotImplementedException("PluginBatchable.GetPerBatchUPL");
        }

        //public List<UserProvisioningRequest> GetPerBatchUPR(){throw new global::System.NotImplementedException("PluginBatchable.GetPerBatchUPR");}
        public Map<Id, SObject> GetUprToNewUplMap()
        {
            throw new global::System.NotImplementedException("PluginBatchable.GetUprToNewUplMap");
        }

        public bool HasFlow()
        {
            throw new global::System.NotImplementedException("PluginBatchable.HasFlow");
        }

        public bool HasFlowOrApex()
        {
            throw new global::System.NotImplementedException("PluginBatchable.HasFlowOrApex");
        }

        public void PostBatchProcessing()
        {
            throw new global::System.NotImplementedException("PluginBatchable.PostBatchProcessing");
        }

        public Database.QueryLocator Start(Database.BatchableContext BC)
        {
            throw new global::System.NotImplementedException("PluginBatchable.Start");
        }
    }
}