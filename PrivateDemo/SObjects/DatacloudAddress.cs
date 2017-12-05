namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DatacloudAddress : SObject
	{
		public string ExternalId {set;get;}

		public string AddressLine1 {set;get;}

		public string AddressLine2 {set;get;}

		public string City {set;get;}

		public string State {set;get;}

		public string Country {set;get;}

		public string PostalCode {set;get;}

		public string Latitude {set;get;}

		public string Longitude {set;get;}

		public string GeoAccuracyCode {set;get;}

		public string GeoAccuracyNum {set;get;}
	}
}
