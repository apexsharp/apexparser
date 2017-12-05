namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ActionLinkGroupTemplate : SObject
	{
		public bool IsDeleted {set;get;}

		public string DeveloperName {set;get;}

		public string Language {set;get;}

		public string MasterLabel {set;get;}

		public string NamespacePrefix {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ExecutionsAllowed {set;get;}

		public int HoursUntilExpiration {set;get;}

		public string Category {set;get;}

		public bool IsPublished {set;get;}
	}
}
