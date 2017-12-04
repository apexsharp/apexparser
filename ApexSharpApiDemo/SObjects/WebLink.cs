namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class WebLink : SObject
	{
		public string PageOrSobjectType {set;get;}
		public string Name {set;get;}
		public bool IsProtected {set;get;}
		public string Url {set;get;}
		public string EncodingKey {set;get;}
		public string LinkType {set;get;}
		public string OpenType {set;get;}
		public int Height {set;get;}
		public int Width {set;get;}
		public bool ShowsLocation {set;get;}
		public bool HasScrollbars {set;get;}
		public bool HasToolbar {set;get;}
		public bool HasMenubar {set;get;}
		public bool ShowsStatus {set;get;}
		public bool IsResizable {set;get;}
		public string Position {set;get;}
		public string ScontrolId {set;get;}
		public string MasterLabel {set;get;}
		public string Description {set;get;}
		public string DisplayType {set;get;}
		public bool RequireRowSelection {set;get;}
		public string NamespacePrefix {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
