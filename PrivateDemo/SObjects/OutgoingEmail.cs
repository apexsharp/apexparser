namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OutgoingEmail : SObject
	{
		public string ExternalId {set;get;}

		public string ValidatedFromAddress {set;get;}

		public string ToAddress {set;get;}

		public string CcAddress {set;get;}

		public string BccAddress {set;get;}

		public string Subject {set;get;}

		public string TextBody {set;get;}

		public string HtmlBody {set;get;}

		public string RelatedToId {set;get;}

		public Account RelatedTo {set;get;}

		public string WhoId {set;get;}

		public Contact Who {set;get;}

		public string EmailTemplateId {set;get;}

		public EmailTemplate EmailTemplate {set;get;}
	}
}
