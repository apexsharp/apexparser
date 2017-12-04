namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmbeddedServiceDetail : SObject
	{
		public string DurableId {set;get;}
		public string Site {set;get;}
		public bool IsPrechatEnabled {set;get;}
		public string PrimaryColor {set;get;}
		public string SecondaryColor {set;get;}
		public string ContrastPrimaryColor {set;get;}
		public string NavBarColor {set;get;}
		public string Font {set;get;}
	}
}
