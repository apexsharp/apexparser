namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CampaignMemberStatus : SObject
	{
		public bool IsDeleted {set;get;}
		public string CampaignId {set;get;}
		public string Label {set;get;}
		public int SortOrder {set;get;}
		public bool IsDefault {set;get;}
		public bool HasResponded {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
