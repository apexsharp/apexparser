namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Idea : SObject
	{
		public bool IsDeleted {set;get;}
		public string Title {set;get;}
		public string RecordTypeId {set;get;}
		public RecordType RecordType {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
		public string CommunityId {set;get;}
		public Community Community {set;get;}
		public string Body {set;get;}
		public int NumComments {set;get;}
		public double VoteScore {set;get;}
		public double VoteTotal {set;get;}
		public string Categories {set;get;}
		public string Status {set;get;}
		public DateTime LastCommentDate {set;get;}
		public string LastCommentId {set;get;}
		public IdeaComment LastComment {set;get;}
		public string ParentIdeaId {set;get;}
		public Idea ParentIdea {set;get;}
		public bool IsHtml {set;get;}
		public bool IsMerged {set;get;}
		public string AttachmentName {set;get;}
		public string AttachmentContentType {set;get;}
		public int AttachmentLength {set;get;}
		public string AttachmentBody {set;get;}
		public string CreatorFullPhotoUrl {set;get;}
		public string CreatorSmallPhotoUrl {set;get;}
		public string CreatorName {set;get;}
	}
}
