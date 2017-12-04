namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseComment : SObject
	{
		public string ParentId {set;get;}
		public Case Parent {set;get;}
		public bool IsPublished {set;get;}
		public string CommentBody {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public bool IsDeleted {set;get;}
	}
}
