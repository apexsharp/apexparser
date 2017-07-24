using Apex.System;

namespace Apex.Cache
{
    public class Session
    {
        public Session()
        {
            throw new global::System.NotImplementedException("Session");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Session.Clone");
        }

        public static bool Contains(string key)
        {
            throw new global::System.NotImplementedException("Session.Contains");
        }

        public static object Get(string key)
        {
            throw new global::System.NotImplementedException("Session.Get");
        }

        public static long GetAvgGetTime()
        {
            throw new global::System.NotImplementedException("Session.GetAvgGetTime");
        }

        public static long GetAvgValueSize()
        {
            throw new global::System.NotImplementedException("Session.GetAvgValueSize");
        }

        public static double GetCapacity()
        {
            throw new global::System.NotImplementedException("Session.GetCapacity");
        }

        public static Set<String> GetKeys()
        {
            throw new global::System.NotImplementedException("Session.GetKeys");
        }

        public static long GetMaxGetTime()
        {
            throw new global::System.NotImplementedException("Session.GetMaxGetTime");
        }

        public static long GetMaxValueSize()
        {
            throw new global::System.NotImplementedException("Session.GetMaxValueSize");
        }

        public static double GetMissRate()
        {
            throw new global::System.NotImplementedException("Session.GetMissRate");
        }

        public static string GetName()
        {
            throw new global::System.NotImplementedException("Session.GetName");
        }

        public static long GetNumKeys()
        {
            throw new global::System.NotImplementedException("Session.GetNumKeys");
        }

        public static Cache.SessionPartition GetPartition(string partitionName)
        {
            throw new global::System.NotImplementedException("Session.GetPartition");
        }

        public static bool IsAvailable()
        {
            throw new global::System.NotImplementedException("Session.IsAvailable");
        }

        public static void Put(string key, object value)
        {
            throw new global::System.NotImplementedException("Session.Put");
        }

        public static void Put(string key, object value, int ttlSecs)
        {
            throw new global::System.NotImplementedException("Session.Put");
        }

        public static void Put(string key, object value, int ttlSecs, Cache.Visibility visibility, bool immutable)
        {
            throw new global::System.NotImplementedException("Session.Put");
        }

        public static void Put(string key, object value, Cache.Visibility visibility)
        {
            throw new global::System.NotImplementedException("Session.Put");
        }

        public static bool Remove(string key)
        {
            throw new global::System.NotImplementedException("Session.Remove");
        }
    }
}