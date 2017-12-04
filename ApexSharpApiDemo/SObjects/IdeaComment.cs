namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class IdeaComment : SObject
	{
		public string IdeaId {set;get;}
		public Idea Idea {set;get;}
		public string CommunityId {set;get;}
		public string CommentBody {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public bool IsDeleted {set;get;}
		public bool IsHtml {set;get;}
		public string CreatorFullPhotoUrl {set;get;}
		public string CreatorSmallPhotoUrl {set;get;}
		public string CreatorName {set;get;}
		public int UpVotes {set;get;}
	}
}
