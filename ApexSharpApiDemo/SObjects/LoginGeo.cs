namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class LoginGeo : SObject
	{
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime LoginTime {set;get;}
		public string CountryIso {set;get;}
		public string Country {set;get;}
		public double Latitude {set;get;}
		public double Longitude {set;get;}
		public string City {set;get;}
		public string PostalCode {set;get;}
		public string Subdivision {set;get;}
	}
}
