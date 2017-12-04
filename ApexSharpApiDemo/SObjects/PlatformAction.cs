namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PlatformAction : SObject
	{
		public string ExternalId {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string Label {set;get;}
		public string Type {set;get;}
		public string Subtype {set;get;}
		public string ApiName {set;get;}
		public string ActionTarget {set;get;}
		public string ActionTargetType {set;get;}
		public string ConfirmationMessage {set;get;}
		public string GroupId {set;get;}
		public bool IsGroupDefault {set;get;}
		public string Category {set;get;}
		public string InvocationStatus {set;get;}
		public string InvokedByUserId {set;get;}
		public User InvokedByUser {set;get;}
		public string SourceEntity {set;get;}
		public string ActionListContext {set;get;}
		public string DeviceFormat {set;get;}
		public string IconContentType {set;get;}
		public int IconHeight {set;get;}
		public int IconWidth {set;get;}
		public string IconUrl {set;get;}
		public bool IsMassAction {set;get;}
		public string PrimaryColor {set;get;}
		public string RelatedSourceEntity {set;get;}
		public string Section {set;get;}
		public string RelatedListRecordId {set;get;}
	}
}
