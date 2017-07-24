namespace Apex.System
{
    public class WebServiceCallout
    {
        public static object BeginInvoke<T>(object stub, object request, object returnType, object continuation,
            List<T> info)
        {
            throw new global::System.NotImplementedException("WebServiceCallout.BeginInvoke");
        }

        public static object EndInvoke(object future)
        {
            throw new global::System.NotImplementedException("WebServiceCallout.EndInvoke");
        }

        public static void Invoke<T, K>(object stub, object request, Map<T, K> response, List<T> info)
        {
            throw new global::System.NotImplementedException("WebServiceCallout.Invoke");
        }
    }
}