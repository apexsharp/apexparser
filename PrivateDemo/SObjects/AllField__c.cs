namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AllField__c : SObject
	{
		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string AuttoNumberDemo__c {set;get;}

		public bool CheckboxDemo__c {set;get;}

		public double CurrencyDemo__c {set;get;}

		public DateTime DateDemo__c {set;get;}

		public DateTime DateTimeDemo__c {set;get;}
	}
}
