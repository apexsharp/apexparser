namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PlatformCachePartitionType : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string PlatformCachePartitionId {set;get;}
		public PlatformCachePartition PlatformCachePartition {set;get;}
		public string CacheType {set;get;}
		public int AllocatedCapacity {set;get;}
		public int AllocatedPurchasedCapacity {set;get;}
		public int AllocatedTrialCapacity {set;get;}
	}
}
