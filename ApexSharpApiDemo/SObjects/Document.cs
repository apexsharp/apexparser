namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Document : SObject
	{
		public string FolderId {set;get;}
		public Folder Folder {set;get;}
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public string DeveloperName {set;get;}
		public string NamespacePrefix {set;get;}
		public string ContentType {set;get;}
		public string Type {set;get;}
		public bool IsPublic {set;get;}
		public int BodyLength {set;get;}
		public string Body {set;get;}
		public string Url {set;get;}
		public string Description {set;get;}
		public string Keywords {set;get;}
		public bool IsInternalUseOnly {set;get;}
		public string AuthorId {set;get;}
		public User Author {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public bool IsBodySearchable {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
	}
}
