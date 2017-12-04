namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CustomBrandAsset : SObject
	{
		public string CustomBrandId {set;get;}
		public CustomBrand CustomBrand {set;get;}
		public string AssetCategory {set;get;}
		public string TextAsset {set;get;}
		public string ForeignKeyAssetId {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
	}
}
