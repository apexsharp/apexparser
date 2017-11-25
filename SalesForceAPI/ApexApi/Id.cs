namespace SalesForceAPI.Apex
{
    public class Id : System.IEquatable<Id>
    {
        private string _id;
        public static implicit operator Id(string v)
        {
            var id = new Id { _id = v };
            return id;
        }

        public override string ToString()
        {
            return _id;
        }

        public void AddError(object msg)
        {
            throw new global::System.NotImplementedException("Id.AddError");
        }

        public void AddError(object msg, bool escape)
        {
            throw new global::System.NotImplementedException("Id.AddError");
        }

        public void AddError(string msg)
        {
            throw new global::System.NotImplementedException("Id.AddError");
        }

        public void AddError(string msg, bool escape)
        {
            throw new global::System.NotImplementedException("Id.AddError");
        }

        //    public SObjectType GetSobjectType() { throw new global::System.NotImplementedException("Id.GetSobjectType"); }
        public static Id ValueOf(string v)
        {
            var id = new Id { _id = v };
            return id;
        }

        public bool Equals(string o) => _id == o;

        public bool Equals(Id other) => other != null && other._id == _id;

        public override bool Equals(object obj) => Equals(obj as Id);

        public override int GetHashCode() => $"{_id}".GetHashCode();
    }
}