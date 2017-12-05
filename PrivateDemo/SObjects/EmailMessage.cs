namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailMessage : SObject
	{
		public string ParentId {set;get;}

		public Case Parent {set;get;}

		public string ActivityId {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string TextBody {set;get;}

		public string HtmlBody {set;get;}

		public string Headers {set;get;}

		public string Subject {set;get;}

		public string FromName {set;get;}

		public string FromAddress {set;get;}

		public string ValidatedFromAddress {set;get;}

		public string ToAddress {set;get;}

		public string CcAddress {set;get;}

		public string BccAddress {set;get;}

		public bool Incoming {set;get;}

		public bool HasAttachment {set;get;}

		public string Status {set;get;}

		public DateTime MessageDate {set;get;}

		public bool IsDeleted {set;get;}

		public string ReplyToEmailMessageId {set;get;}

		public EmailMessage ReplyToEmailMessage {set;get;}

		public bool IsExternallyVisible {set;get;}

		public string MessageIdentifier {set;get;}

		public string ThreadIdentifier {set;get;}

		public bool IsClientManaged {set;get;}

		public string RelatedToId {set;get;}

		public Account RelatedTo {set;get;}
	}
}
