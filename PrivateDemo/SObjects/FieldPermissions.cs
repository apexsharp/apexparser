namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FieldPermissions : SObject
	{
		public string ParentId {set;get;}

		public PermissionSet Parent {set;get;}

		public string SobjectType {set;get;}

		public string Field {set;get;}

		public bool PermissionsEdit {set;get;}

		public bool PermissionsRead {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
