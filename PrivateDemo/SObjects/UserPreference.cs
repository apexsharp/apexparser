namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserPreference : SObject
	{
		public string UserId {set;get;}

		public string Preference {set;get;}

		public string Value {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
