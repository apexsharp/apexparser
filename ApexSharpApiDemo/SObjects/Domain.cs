namespace ApexSharpApiDemo.SObjects
{
    using Apex.System;
    using ApexSharpApi.ApexApi;

    public class Domain : SObject
    {
        public string DomainType { set; get; }
        //public string Domain {set;get;}
        public bool OptionsExternalHttps { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedById { set; get; }
        public User CreatedBy { set; get; }
        public DateTime LastModifiedDate { set; get; }
        public string LastModifiedById { set; get; }
        public User LastModifiedBy { set; get; }
        public DateTime SystemModstamp { set; get; }
    }
}
