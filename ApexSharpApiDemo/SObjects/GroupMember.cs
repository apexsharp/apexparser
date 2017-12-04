namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class GroupMember : SObject
	{
		public string GroupId {set;get;}
		public Group Group {set;get;}
		public string UserOrGroupId {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
