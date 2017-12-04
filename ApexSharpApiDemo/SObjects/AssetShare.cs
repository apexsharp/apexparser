namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AssetShare : SObject
	{
		public string AssetId {set;get;}
		public Asset Asset {set;get;}
		public string UserOrGroupId {set;get;}
		public Group UserOrGroup {set;get;}
		public string AssetAccessLevel {set;get;}
		public string RowCause {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public bool IsDeleted {set;get;}
	}
}
