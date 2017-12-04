namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserEntityAccess : SObject
	{
		public string DurableId {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public bool IsMergeable {set;get;}
		public bool IsUpdatable {set;get;}
		public bool IsActivateable {set;get;}
		public bool IsReadable {set;get;}
		public bool IsCreatable {set;get;}
		public bool IsEditable {set;get;}
		public bool IsDeletable {set;get;}
		public bool IsUndeletable {set;get;}
		public bool IsFlsUpdatable {set;get;}
		public string EntityDefinitionId {set;get;}
	}
}
