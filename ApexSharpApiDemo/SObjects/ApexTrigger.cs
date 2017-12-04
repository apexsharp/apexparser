namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexTrigger : SObject
	{
		public string NamespacePrefix {set;get;}
		public string Name {set;get;}
		public string TableEnumOrId {set;get;}
		public bool UsageBeforeInsert {set;get;}
		public bool UsageAfterInsert {set;get;}
		public bool UsageBeforeUpdate {set;get;}
		public bool UsageAfterUpdate {set;get;}
		public bool UsageBeforeDelete {set;get;}
		public bool UsageAfterDelete {set;get;}
		public bool UsageIsBulk {set;get;}
		public bool UsageAfterUndelete {set;get;}
		public double ApiVersion {set;get;}
		public string Status {set;get;}
		public bool IsValid {set;get;}
		public double BodyCrc {set;get;}
		public string Body {set;get;}
		public int LengthWithoutComments {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
