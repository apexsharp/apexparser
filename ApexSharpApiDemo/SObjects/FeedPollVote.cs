namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedPollVote : SObject
	{
		public string FeedItemId {set;get;}
		public string ChoiceId {set;get;}
		public FeedPollChoice Choice {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public bool IsDeleted {set;get;}
	}
}
