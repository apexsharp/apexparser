namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ObjectPermissions : SObject
	{
		public string ParentId {set;get;}

		public PermissionSet Parent {set;get;}

		public string SobjectType {set;get;}

		public bool PermissionsCreate {set;get;}

		public bool PermissionsRead {set;get;}

		public bool PermissionsEdit {set;get;}

		public bool PermissionsDelete {set;get;}

		public bool PermissionsViewAllRecords {set;get;}

		public bool PermissionsModifyAllRecords {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
