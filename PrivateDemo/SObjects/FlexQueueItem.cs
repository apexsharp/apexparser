namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FlexQueueItem : SObject
	{
		public string FlexQueueItemId {set;get;}

		public string JobType {set;get;}

		public string AsyncApexJobId {set;get;}

		public AsyncApexJob AsyncApexJob {set;get;}

		public int JobPosition {set;get;}
	}
}
