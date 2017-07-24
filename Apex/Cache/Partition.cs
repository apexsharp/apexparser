using Apex.System;

namespace Apex.Cache
{
    public class Partition
    {
        public Partition()
        {
            throw new global::System.NotImplementedException("Partition");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Partition.Clone");
        }

        public bool Contains(string key)
        {
            throw new global::System.NotImplementedException("Partition.Contains");
        }

        public static string CreateFullyQualifiedKey(string namespaceApex, string partition, string key)
        {
            throw new global::System.NotImplementedException("Partition.CreateFullyQualifiedKey");
        }

        public static string CreateFullyQualifiedPartition(string namespaceApex, string partition)
        {
            throw new global::System.NotImplementedException("Partition.CreateFullyQualifiedPartition");
        }

        public object Get(string key)
        {
            throw new global::System.NotImplementedException("Partition.Get");
        }

        public long GetAvgGetTime()
        {
            throw new global::System.NotImplementedException("Partition.GetAvgGetTime");
        }

        public long GetAvgValueSize()
        {
            throw new global::System.NotImplementedException("Partition.GetAvgValueSize");
        }

        public double GetCapacity()
        {
            throw new global::System.NotImplementedException("Partition.GetCapacity");
        }

        public Set<String> GetKeys()
        {
            throw new global::System.NotImplementedException("Partition.GetKeys");
        }

        public long GetMaxGetTime()
        {
            throw new global::System.NotImplementedException("Partition.GetMaxGetTime");
        }

        public long GetMaxValueSize()
        {
            throw new global::System.NotImplementedException("Partition.GetMaxValueSize");
        }

        public double GetMissRate()
        {
            throw new global::System.NotImplementedException("Partition.GetMissRate");
        }

        public string GetName()
        {
            throw new global::System.NotImplementedException("Partition.GetName");
        }

        public long GetNumKeys()
        {
            throw new global::System.NotImplementedException("Partition.GetNumKeys");
        }

        public bool IsAvailable()
        {
            throw new global::System.NotImplementedException("Partition.IsAvailable");
        }

        public void Put(string key, object value)
        {
            throw new global::System.NotImplementedException("Partition.Put");
        }

        public void Put(string key, object value, int ttlSecs)
        {
            throw new global::System.NotImplementedException("Partition.Put");
        }

        public void Put(string key, object value, int ttlSecs, Cache.Visibility visibility, bool immutable)
        {
            throw new global::System.NotImplementedException("Partition.Put");
        }

        public void Put(string key, object value, Cache.Visibility visibility)
        {
            throw new global::System.NotImplementedException("Partition.Put");
        }

        public bool Remove(string key)
        {
            throw new global::System.NotImplementedException("Partition.Remove");
        }

        public static void ValidateKey(bool isDefault, string key)
        {
            throw new global::System.NotImplementedException("Partition.ValidateKey");
        }

        public static void ValidateKeyValue(bool isDefault, string key, object value)
        {
            throw new global::System.NotImplementedException("Partition.ValidateKeyValue");
        }

        public static void ValidateKeys(bool isDefault, List<string> keys)
        {
            throw new global::System.NotImplementedException("Partition.ValidateKeys");
        }

        public static void ValidatePartitionName(string name)
        {
            throw new global::System.NotImplementedException("Partition.ValidatePartitionName");
        }
    }
}