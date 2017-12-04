namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class QueueSobject : SObject
	{
		public string QueueId {set;get;}
		public Group Queue {set;get;}
		public string SobjectType {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
