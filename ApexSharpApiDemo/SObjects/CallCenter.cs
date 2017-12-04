namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CallCenter : SObject
	{
		public string Name {set;get;}
		public string InternalName {set;get;}
		public double Version {set;get;}
		public string AdapterUrl {set;get;}
		public string CustomSettings {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
	}
}
