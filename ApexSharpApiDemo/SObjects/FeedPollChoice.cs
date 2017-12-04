namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedPollChoice : SObject
	{
		public string FeedItemId {set;get;}
		public int Position {set;get;}
		public string ChoiceBody {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public bool IsDeleted {set;get;}
	}
}
