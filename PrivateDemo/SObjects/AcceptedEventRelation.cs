namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AcceptedEventRelation : SObject
	{
		public string RelationId {set;get;}

		public Contact Relation {set;get;}

		public string EventId {set;get;}

		public Event Event {set;get;}

		public DateTime RespondedDate {set;get;}

		public string Response {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}

		public string Type {set;get;}
	}
}
