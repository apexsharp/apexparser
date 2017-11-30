namespace Apex.System
{
    public class RestRequest
    {
        public Blob RequestBody { set; get; }
        public RestRequest()
        {

        }

        public void AddHeader(string name, string value)
        {
            throw new global::System.NotImplementedException("RestRequest.AddHeader");
        }

        public void AddParameter(string name, string value)
        {
            throw new global::System.NotImplementedException("RestRequest.AddParameter");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("RestRequest.Clone");
        }
    }
}