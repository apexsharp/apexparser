namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ActionLinkTemplate : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string ActionLinkGroupTemplateId {set;get;}
		public ActionLinkGroupTemplate ActionLinkGroupTemplate {set;get;}
		public string LabelKey {set;get;}
		public string Method {set;get;}
		public string LinkType {set;get;}
		public int Position {set;get;}
		public bool IsConfirmationRequired {set;get;}
		public bool IsGroupDefault {set;get;}
		public string UserVisibility {set;get;}
		public string UserAlias {set;get;}
		public string Label {set;get;}
		public string ActionUrl {set;get;}
		public string RequestBody {set;get;}
		public string Headers {set;get;}
	}
}
