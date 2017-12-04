namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PermissionSetLicenseAssign : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string PermissionSetLicenseId {set;get;}
		public PermissionSetLicense PermissionSetLicense {set;get;}
		public string AssigneeId {set;get;}
		public User Assignee {set;get;}
	}
}
