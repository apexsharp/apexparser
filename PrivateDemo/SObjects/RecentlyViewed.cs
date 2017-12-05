namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class RecentlyViewed : SObject
	{
		public string Name {set;get;}

		public string LastName {set;get;}

		public string FirstName {set;get;}

		public string Type {set;get;}

		public string Alias {set;get;}

		public string UserRoleId {set;get;}

		public UserRole UserRole {set;get;}

		public string RecordTypeId {set;get;}

		public RecordType RecordType {set;get;}

		public bool IsActive {set;get;}

		public string ProfileId {set;get;}

		public Profile Profile {set;get;}

		public string Title {set;get;}

		public string Email {set;get;}

		public string Phone {set;get;}

		public string NameOrAlias {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string Language {set;get;}
	}
}
