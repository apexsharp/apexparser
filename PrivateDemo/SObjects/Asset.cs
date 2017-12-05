namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Asset : SObject
	{
		public string ContactId {set;get;}

		public Contact Contact {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string ParentId {set;get;}

		public Asset Parent {set;get;}

		public string RootAssetId {set;get;}

		public Asset RootAsset {set;get;}

		public string Product2Id {set;get;}

		public Product2 Product2 {set;get;}

		public bool IsCompetitorProduct {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public string SerialNumber {set;get;}

		public DateTime InstallDate {set;get;}

		public DateTime PurchaseDate {set;get;}

		public DateTime UsageEndDate {set;get;}

		public string Status {set;get;}

		public double Price {set;get;}

		public double Quantity {set;get;}

		public string Description {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string AssetProvidedById {set;get;}

		public Account AssetProvidedBy {set;get;}

		public string AssetServicedById {set;get;}

		public Account AssetServicedBy {set;get;}

		public bool IsInternal {set;get;}

		public int AssetLevel {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}
	}
}
