namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DatacloudContact : SObject
	{
		public string ExternalId {set;get;}

		public string CompanyId {set;get;}

		public string ContactId {set;get;}

		public string CompanyName {set;get;}

		public string Title {set;get;}

		public bool IsInactive {set;get;}

		public string FirstName {set;get;}

		public string LastName {set;get;}

		public string Phone {set;get;}

		public string Email {set;get;}

		public string Street {set;get;}

		public string City {set;get;}

		public string State {set;get;}

		public string Country {set;get;}

		public string Zip {set;get;}

		public string Department {set;get;}

		public string Level {set;get;}

		public bool IsOwned {set;get;}

		public DateTime UpdatedDate {set;get;}

		public bool IsInCrm {set;get;}
	}
}
