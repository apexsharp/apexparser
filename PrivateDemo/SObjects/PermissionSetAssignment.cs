namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PermissionSetAssignment : SObject
	{
		public string PermissionSetId {set;get;}

		public PermissionSet PermissionSet {set;get;}

		public string AssigneeId {set;get;}

		public User Assignee {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
