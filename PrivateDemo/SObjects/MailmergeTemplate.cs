namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class MailmergeTemplate : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public string Description {set;get;}

		public string Filename {set;get;}

		public int BodyLength {set;get;}

		public string Body {set;get;}

		public DateTime LastUsedDate {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool SecurityOptionsAttachmentScannedForXSS {set;get;}

		public bool SecurityOptionsAttachmentHasXSSThreat {set;get;}

		public bool SecurityOptionsAttachmentScannedforFlash {set;get;}

		public bool SecurityOptionsAttachmentHasFlash {set;get;}
	}
}
