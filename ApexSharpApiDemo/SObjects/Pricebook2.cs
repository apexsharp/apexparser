namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Pricebook2 : SObject
	{
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
		public bool IsActive {set;get;}
		public bool IsArchived {set;get;}
		public string Description {set;get;}
		public bool IsStandard {set;get;}
	}
}
