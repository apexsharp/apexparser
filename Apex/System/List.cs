using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Apex.Schema;
using SalesForceAPI.Apex;

namespace Apex.System
{
    public class List<T> : global::System.Collections.Generic.List<T>
    {
        public List()
        {
        }

        public List(int param1)
        {
            throw new global::System.NotImplementedException("List");
        }

        public T this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        public void Add(T item)
        {
            base.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
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
            return base.Count;
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
            throw new global::System.NotImplementedException("List.Clear");
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

        public Id Get(int index)
        {
            throw new global::System.NotImplementedException("List.Get");
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
            return base.Count == 0;
        }

        public Iterable Iterator()
        {
            throw new global::System.NotImplementedException("List.Iterator");
        }

        public string Remove(int index)
        {
            throw new global::System.NotImplementedException("List.Remove");
        }

        public void Set(int index, object value)
        {
            throw new global::System.NotImplementedException("List.Set");
        }

        public void Sort()
        {
            throw new global::System.NotImplementedException("List.Sort");
        }
    }
}