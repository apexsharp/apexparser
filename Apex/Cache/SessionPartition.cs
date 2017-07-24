using Apex.System;

namespace Apex.Cache
{
    public class SessionPartition
    {
        public SessionPartition(string fullyQualifiedPartitionName)
        {
            throw new global::System.NotImplementedException("SessionPartition");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("SessionPartition.Clone");
        }

        public bool Contains(string key)
        {
            throw new global::System.NotImplementedException("SessionPartition.Contains");
        }

        public static string CreateFullyQualifiedKey(string namespaceApex, string partition, string key)
        {
            throw new global::System.NotImplementedException("SessionPartition.CreateFullyQualifiedKey");
        }

        public static string CreateFullyQualifiedPartition(string namespaceApex, string partition)
        {
            throw new global::System.NotImplementedException("SessionPartition.CreateFullyQualifiedPartition");
        }

        public object Get(string key)
        {
            throw new global::System.NotImplementedException("SessionPartition.Get");
        }

        public long GetAvgGetTime()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetAvgGetTime");
        }

        public long GetAvgValueSize()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetAvgValueSize");
        }

        public double GetCapacity()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetCapacity");
        }

        public Set<String> GetKeys()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetKeys");
        }

        public long GetMaxGetTime()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetMaxGetTime");
        }

        public long GetMaxValueSize()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetMaxValueSize");
        }

        public double GetMissRate()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetMissRate");
        }

        public string GetName()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetName");
        }

        public long GetNumKeys()
        {
            throw new global::System.NotImplementedException("SessionPartition.GetNumKeys");
        }

        public bool IsAvailable()
        {
            throw new global::System.NotImplementedException("SessionPartition.IsAvailable");
        }

        public void Put(string key, object value)
        {
            throw new global::System.NotImplementedException("SessionPartition.Put");
        }

        public void Put(string key, object value, int ttlSecs)
        {
            throw new global::System.NotImplementedException("SessionPartition.Put");
        }

        public void Put(string key, object value, int ttlSecs, Cache.Visibility visibility, bool immutable)
        {
            throw new global::System.NotImplementedException("SessionPartition.Put");
        }

        public void Put(string key, object value, Cache.Visibility visibility)
        {
            throw new global::System.NotImplementedException("SessionPartition.Put");
        }

        public bool Remove(string key)
        {
            throw new global::System.NotImplementedException("SessionPartition.Remove");
        }

        public static void ValidateKey(bool isDefault, string key)
        {
            throw new global::System.NotImplementedException("SessionPartition.ValidateKey");
        }

        public static void ValidateKeyValue(bool isDefault, string key, object value)
        {
            throw new global::System.NotImplementedException("SessionPartition.ValidateKeyValue");
        }

        public static void ValidateKeys(bool isDefault, List<string> keys)
        {
            throw new global::System.NotImplementedException("SessionPartition.ValidateKeys");
        }

        public static void ValidatePartitionName(string name)
        {
            throw new global::System.NotImplementedException("SessionPartition.ValidatePartitionName");
        }
    }
}