using Apex.System;

namespace Apex.Cache
{
    public class OrgPartition
    {
        public OrgPartition(string fullyQualifiedPartitionName)
        {
            throw new global::System.NotImplementedException("OrgPartition");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("OrgPartition.Clone");
        }

        public bool Contains(string key)
        {
            throw new global::System.NotImplementedException("OrgPartition.Contains");
        }

        public static string CreateFullyQualifiedKey(string namespaceApex, string partition, string key)
        {
            throw new global::System.NotImplementedException("OrgPartition.CreateFullyQualifiedKey");
        }

        public static string CreateFullyQualifiedPartition(string namespaceApex, string partition)
        {
            throw new global::System.NotImplementedException("OrgPartition.CreateFullyQualifiedPartition");
        }

        public object Get(string key)
        {
            throw new global::System.NotImplementedException("OrgPartition.Get");
        }

        public long GetAvgGetTime()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetAvgGetTime");
        }

        public long GetAvgValueSize()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetAvgValueSize");
        }

        public double GetCapacity()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetCapacity");
        }

        public Set<String> GetKeys()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetKeys");
        }

        public long GetMaxGetTime()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetMaxGetTime");
        }

        public long GetMaxValueSize()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetMaxValueSize");
        }

        public double GetMissRate()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetMissRate");
        }

        public string GetName()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetName");
        }

        public long GetNumKeys()
        {
            throw new global::System.NotImplementedException("OrgPartition.GetNumKeys");
        }

        public bool IsAvailable()
        {
            throw new global::System.NotImplementedException("OrgPartition.IsAvailable");
        }

        public void Put(string key, object value)
        {
            throw new global::System.NotImplementedException("OrgPartition.Put");
        }

        public void Put(string key, object value, int ttlSecs)
        {
            throw new global::System.NotImplementedException("OrgPartition.Put");
        }

        public void Put(string key, object value, int ttlSecs, Cache.Visibility visibility, bool immutable)
        {
            throw new global::System.NotImplementedException("OrgPartition.Put");
        }

        public void Put(string key, object value, Cache.Visibility visibility)
        {
            throw new global::System.NotImplementedException("OrgPartition.Put");
        }

        public bool Remove(string key)
        {
            throw new global::System.NotImplementedException("OrgPartition.Remove");
        }

        public static void ValidateKey(bool isDefault, string key)
        {
            throw new global::System.NotImplementedException("OrgPartition.ValidateKey");
        }

        public static void ValidateKeyValue(bool isDefault, string key, object value)
        {
            throw new global::System.NotImplementedException("OrgPartition.ValidateKeyValue");
        }

        public static void ValidateKeys(bool isDefault, List<string> keys)
        {
            throw new global::System.NotImplementedException("OrgPartition.ValidateKeys");
        }

        public static void ValidatePartitionName(string name)
        {
            throw new global::System.NotImplementedException("OrgPartition.ValidatePartitionName");
        }
    }
}