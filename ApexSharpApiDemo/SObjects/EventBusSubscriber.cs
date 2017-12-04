namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EventBusSubscriber : SObject
	{
		public string ExternalId {set;get;}
		public string Name {set;get;}
		public string Type {set;get;}
		public string Topic {set;get;}
		public int Position {set;get;}
		public int Tip {set;get;}
		public string Status {set;get;}
	}
}
