namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedLike : SObject
	{
		public string FeedItemId {set;get;}

		public string FeedEntityId {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public bool IsDeleted {set;get;}

		public string InsertedById {set;get;}

		public User InsertedBy {set;get;}
	}
}
