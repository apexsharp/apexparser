namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentVersion : SObject
	{
		public string ContentDocumentId {set;get;}
		public ContentDocument ContentDocument {set;get;}
		public bool IsLatest {set;get;}
		public string ContentUrl {set;get;}
		public string ContentBodyId {set;get;}
		public ContentBody ContentBody {set;get;}
		public string VersionNumber {set;get;}
		public string Title {set;get;}
		public string Description {set;get;}
		public string ReasonForChange {set;get;}
		public string SharingOption {set;get;}
		public string PathOnClient {set;get;}
		public int RatingCount {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime ContentModifiedDate {set;get;}
		public string ContentModifiedById {set;get;}
		public User ContentModifiedBy {set;get;}
		public int PositiveRatingCount {set;get;}
		public int NegativeRatingCount {set;get;}
		public int FeaturedContentBoost {set;get;}
		public DateTime FeaturedContentDate {set;get;}
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string TagCsv {set;get;}
		public string FileType {set;get;}
		public string PublishStatus {set;get;}
		public string VersionData {set;get;}
		public int ContentSize {set;get;}
		public string FileExtension {set;get;}
		public string FirstPublishLocationId {set;get;}
		public Account FirstPublishLocation {set;get;}
		public string Origin {set;get;}
		public string ContentLocation {set;get;}
		public string TextPreview {set;get;}
		public string ExternalDocumentInfo1 {set;get;}
		public string ExternalDocumentInfo2 {set;get;}
		public string ExternalDataSourceId {set;get;}
		public ExternalDataSource ExternalDataSource {set;get;}
		public string Checksum {set;get;}
		public bool IsMajorVersion {set;get;}
		public bool IsAssetEnabled {set;get;}
	}
}
