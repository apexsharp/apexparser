namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedTrackedChange : SObject
	{
		public string FeedItemId {set;get;}
		public string FieldName {set;get;}
		public object OldValue {set;get;}
		public object NewValue {set;get;}
	}
}
