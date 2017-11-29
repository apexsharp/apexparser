using System;
using SalesForceAPI;
using SalesForceAPI.ApexApi;

namespace Apex.System
{
    public class Map<T, K> : global::System.Collections.Generic.SortedDictionary<T, K>
    {
        public Map()
        {
        }

        public Map(SoqlQuery<K> soqlQuery)
        {
            // make sure that Map<T, K> is Map<ID, SObject>
            if (!typeof(SObject).IsAssignableFrom(typeof(K)) ||
                !typeof(ID).IsAssignableFrom(typeof(T)))
            {
                throw new NotSupportedException("Only Map<ID, SObject> can be initialized via SOQL query data.");
            }

            foreach (object row in soqlQuery.QueryResult.Value)
            {
                var sobj = (SObject)row;
                object key = sobj.Id;
                this[(T)key] = (K)row;
            }
        }

        public Map(List<K> objectList)
        {

        }

        public Map(List<object> param1)
        {
            throw new global::System.NotImplementedException("Map");
        }

        public Map(Map<object, object> param1)
        {
            throw new global::System.NotImplementedException("Map");
        }

        public void Clear()
        {
            throw new global::System.NotImplementedException("Map.Clear");
        }

        public Map<String, String> Clone()
        {
            throw new global::System.NotImplementedException("Map.Clone");
        }

        public bool ContainsKey(object key)
        {
            throw new global::System.NotImplementedException("Map.ContainsKey");
        }

        public Map<String, String> DeepClone()
        {
            throw new global::System.NotImplementedException("Map.DeepClone");
        }

        public bool Equals(object obj)
        {
            throw new global::System.NotImplementedException("Map.Equals");
        }

        public K Get(T key)
        {
            return default(K);
        }

        //public Schema.SObjectType GetSObjectType() { throw new global::System.NotImplementedException("Map.GetSObjectType"); }
        public int HashCode()
        {
            throw new global::System.NotImplementedException("Map.HashCode");
        }

        public bool IsEmpty()
        {
            throw new global::System.NotImplementedException("Map.IsEmpty");
        }

        public Set<T> KeySet()
        {
            return new Set<T>();
        }

        public string Put(object key, object value)
        {
            throw new global::System.NotImplementedException("Map.Put");
        }

        public void PutAll(List<T> entries)
        {
            throw new global::System.NotImplementedException("Map.PutAll");
        }

        public void PutAll(Map<T, K> entries)
        {
            throw new global::System.NotImplementedException("Map.PutAll");
        }

        public string Remove(object key)
        {
            throw new global::System.NotImplementedException("Map.Remove");
        }

        public int Size()
        {
            throw new global::System.NotImplementedException("Map.Size");
        }

        public List<string> Values()
        {
            throw new global::System.NotImplementedException("Map.Values");
        }
    }
}