namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CampaignFeed : SObject
	{
		public string ParentId {set;get;}
		public Campaign Parent {set;get;}
		public string Type {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public int CommentCount {set;get;}
		public int LikeCount {set;get;}
		public string Title {set;get;}
		public string Body {set;get;}
		public string LinkUrl {set;get;}
		public bool IsRichText {set;get;}
		public string RelatedRecordId {set;get;}
		public string InsertedById {set;get;}
		public User InsertedBy {set;get;}
	}
}
