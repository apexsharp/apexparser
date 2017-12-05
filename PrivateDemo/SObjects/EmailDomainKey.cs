namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailDomainKey : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string Selector {set;get;}

		public string Domain {set;get;}

		public string DomainMatch {set;get;}

		public bool IsActive {set;get;}

		public string PublicKey {set;get;}

		public string PrivateKey {set;get;}
	}
}
