namespace Apex.System
{
    public class RestContext
    {
        public static RestRequest Request { get; set; }
        public static RestResponse Response { get; set; }

        public object Clone()
        {
            throw new global::System.NotImplementedException("RestContext.Clone");
        }
    }
}