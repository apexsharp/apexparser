namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailServicesAddress : SObject
	{
		public bool IsActive {set;get;}
		public string LocalPart {set;get;}
		public string EmailDomainName {set;get;}
		public string AuthorizedSenders {set;get;}
		public string RunAsUserId {set;get;}
		public string FunctionId {set;get;}
		public EmailServicesFunction Function {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
