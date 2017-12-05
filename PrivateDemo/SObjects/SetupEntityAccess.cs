namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SetupEntityAccess : SObject
	{
		public string ParentId {set;get;}

		public PermissionSet Parent {set;get;}

		public string SetupEntityId {set;get;}

		public string SetupEntityType {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
