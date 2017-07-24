using Apex.System;

namespace Apex.Database
{
    public class Batchable
    {
        public void Execute(Database.BatchableContext param1, List<object> param2)
        {
            throw new global::System.NotImplementedException("Batchable.Execute");
        }

        public void Finish(Database.BatchableContext param1)
        {
            throw new global::System.NotImplementedException("Batchable.Finish");
        }

        public System.Iterable Start(Database.BatchableContext param1)
        {
            throw new global::System.NotImplementedException("Batchable.Start");
        }
    }
}