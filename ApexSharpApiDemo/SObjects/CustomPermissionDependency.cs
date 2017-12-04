namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CustomPermissionDependency : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string CustomPermissionId {set;get;}
		public CustomPermission CustomPermission {set;get;}
		public string RequiredCustomPermissionId {set;get;}
		public CustomPermission RequiredCustomPermission {set;get;}
	}
}
