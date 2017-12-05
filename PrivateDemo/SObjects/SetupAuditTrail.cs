namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SetupAuditTrail : SObject
	{
		public string Action {set;get;}

		public string Section {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public string Display {set;get;}

		public string DelegateUser {set;get;}

		public string ResponsibleNamespacePrefix {set;get;}
	}
}
