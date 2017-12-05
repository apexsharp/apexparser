namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedComment : SObject
	{
		public string FeedItemId {set;get;}

		public string ParentId {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public DateTime SystemModstamp {set;get;}

		public int Revision {set;get;}

		public string LastEditById {set;get;}

		public DateTime LastEditDate {set;get;}

		public string CommentBody {set;get;}

		public bool IsDeleted {set;get;}

		public string InsertedById {set;get;}

		public User InsertedBy {set;get;}

		public string CommentType {set;get;}

		public string RelatedRecordId {set;get;}

		public bool IsRichText {set;get;}

		public string Status {set;get;}
	}
}
