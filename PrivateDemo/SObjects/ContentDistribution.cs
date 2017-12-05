namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentDistribution : SObject
	{
		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string Name {set;get;}

		public bool IsDeleted {set;get;}

		public string ContentVersionId {set;get;}

		public ContentVersion ContentVersion {set;get;}

		public string ContentDocumentId {set;get;}

		public string RelatedRecordId {set;get;}

		public Account RelatedRecord {set;get;}

		public bool PreferencesAllowPDFDownload {set;get;}

		public bool PreferencesAllowOriginalDownload {set;get;}

		public bool PreferencesPasswordRequired {set;get;}

		public bool PreferencesNotifyOnVisit {set;get;}

		public bool PreferencesLinkLatestVersion {set;get;}

		public bool PreferencesAllowViewInBrowser {set;get;}

		public bool PreferencesExpires {set;get;}

		public bool PreferencesNotifyRndtnComplete {set;get;}

		public DateTime ExpiryDate {set;get;}

		public string Password {set;get;}

		public int ViewCount {set;get;}

		public DateTime FirstViewDate {set;get;}

		public DateTime LastViewDate {set;get;}

		public string DistributionPublicUrl {set;get;}

		public string ContentDownloadUrl {set;get;}

		public string PdfDownloadUrl {set;get;}
	}
}
