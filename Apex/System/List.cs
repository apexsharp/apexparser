using Apex.Schema;
using ApexSharpApi.ApexApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApexSharpApi;

namespace Apex.System
{
    public class List<T> : global::System.Collections.Generic.List<T>
    {
        private global::System.Collections.Generic.List<T> list;
        public List()
        {
            list = new global::System.Collections.Generic.List<T>();
        }

        public List(int size)
        {
            list = new global::System.Collections.Generic.List<T>(size);
        }

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void AddAll(List<T> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
        }

        public int Size()
        {
            return list.Count;
        }

        public void Add(int index, object element)
        {
            throw new global::System.NotImplementedException("List.Add");
        }

        public void AddAll(Set<T> elements)
        {
            throw new global::System.NotImplementedException("List.AddAll");
        }

        public void Clear()
        {
            list.Clear();
        }

        public List<string> Clone()
        {
            throw new global::System.NotImplementedException("List.Clone");
        }

        public List<string> DeepClone()
        {
            throw new global::System.NotImplementedException("List.DeepClone");
        }

        public List<string> DeepClone(bool preserveId)
        {
            throw new global::System.NotImplementedException("List.DeepClone");
        }

        public List<string> DeepClone(bool preserveId, bool preserveReadOnlyTimestamps)
        {
            throw new global::System.NotImplementedException("List.DeepClone");
        }

        public List<string> DeepClone(bool preserveId, bool preserveReadOnlyTimestamps, bool preserveAutoNumbers)
        {
            throw new global::System.NotImplementedException("List.DeepClone");
        }

        public bool Equals(object obj)
        {
            throw new global::System.NotImplementedException("List.Equals");
        }

        public T Get(int index)
        {
            return list[index];
        }

        public SObjectType GetSObjectType()
        {
            throw new global::System.NotImplementedException("List.GetSObjectType");
        }

        public int HashCode()
        {
            throw new global::System.NotImplementedException("List.HashCode");
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public Iterable Iterator()
        {
            throw new global::System.NotImplementedException("List.Iterator");
        }

        public T Remove(int index)
        {
            var value = list[index];
            list.RemoveAt(index);
            return value;
        }

        public void Set(int index, object value)
        {
            throw new global::System.NotImplementedException("List.Set");
        }

        public void Sort()
        {
            list.Sort();
        }

        public static implicit operator List<T>(SoqlQuery<T> query)
        {
            var result = new List<T>();
            foreach (var row in query.QueryResult.Value)
            {
                result.Add(row);
            }

            return result;
        }
    }
}