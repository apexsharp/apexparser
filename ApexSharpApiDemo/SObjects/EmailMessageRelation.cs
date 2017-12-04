namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailMessageRelation : SObject
	{
		public string EmailMessageId {set;get;}
		public EmailMessage EmailMessage {set;get;}
		public string RelationId {set;get;}
		public Contact Relation {set;get;}
		public string RelationType {set;get;}
		public string RelationAddress {set;get;}
		public string RelationObjectType {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public bool IsDeleted {set;get;}
	}
}
