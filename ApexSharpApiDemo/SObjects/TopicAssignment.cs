namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class TopicAssignment : SObject
	{
		public string TopicId {set;get;}
		public Topic Topic {set;get;}
		public string EntityId {set;get;}
		public Account Entity {set;get;}
		public string EntityKeyPrefix {set;get;}
		public string EntityType {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
