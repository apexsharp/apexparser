namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailTemplate : SObject
	{
		public string Name {set;get;}

		public string DeveloperName {set;get;}

		public string NamespacePrefix {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string FolderId {set;get;}

		public Folder Folder {set;get;}

		public string BrandTemplateId {set;get;}

		public string TemplateStyle {set;get;}

		public bool IsActive {set;get;}

		public string TemplateType {set;get;}

		public string Encoding {set;get;}

		public string Description {set;get;}

		public string Subject {set;get;}

		public string HtmlValue {set;get;}

		public string Body {set;get;}

		public int TimesUsed {set;get;}

		public DateTime LastUsedDate {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public double ApiVersion {set;get;}

		public string Markup {set;get;}

		public string UiType {set;get;}

		public string RelatedEntityType {set;get;}
	}
}
