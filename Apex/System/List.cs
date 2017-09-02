using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Apex.Schema;
using SalesForceAPI.Apex;

namespace Apex.System
{
    public class List<T> : IEnumerable
    {
        private readonly global::System.Collections.Generic.List<T> _internalList;
        public T[] objects;


        public List()
        {
            _internalList = new global::System.Collections.Generic.List<T>();
        }

        public List(int param1)
        {
            throw new global::System.NotImplementedException("List");
        }

        public T this[int index]
        {
            get { return _internalList[index]; }
            set { _internalList[index] = value; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            _internalList.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
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
            return _internalList.Count;
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
            return _internalList.Count == 0;
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