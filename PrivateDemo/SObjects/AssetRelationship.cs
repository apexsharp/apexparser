namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AssetRelationship : SObject
	{
		public bool IsDeleted {set;get;}

		public string AssetRelationshipNumber {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string AssetId {set;get;}

		public Asset Asset {set;get;}

		public string RelatedAssetId {set;get;}

		public Asset RelatedAsset {set;get;}

		public DateTime FromDate {set;get;}

		public DateTime ToDate {set;get;}

		public string RelationshipType {set;get;}
	}
}
