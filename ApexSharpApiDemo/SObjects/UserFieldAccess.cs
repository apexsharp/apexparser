namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserFieldAccess : SObject
	{
		public string DurableId {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public bool IsUpdatable {set;get;}
		public bool IsCreatable {set;get;}
		public bool IsAccessible {set;get;}
		public string EntityDefinitionId {set;get;}
		public string FieldDefinitionId {set;get;}
	}
}
