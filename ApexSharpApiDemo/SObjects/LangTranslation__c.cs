namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class LangTranslation__c : SObject
	{
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public string SetupOwnerId {set;get;}
		public Organization SetupOwner {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string Name__c {set;get;}
		public string Eng__c {set;get;}
		public string Ch__c {set;get;}
	}
}
