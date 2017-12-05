namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentDistributionView : SObject
	{
		public string DistributionId {set;get;}

		public ContentDistribution Distribution {set;get;}

		public string ParentViewId {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}

		public bool IsInternal {set;get;}

		public bool IsDownload {set;get;}
	}
}
