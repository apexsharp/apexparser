namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedAttachment : SObject
	{
		public string FeedEntityId {set;get;}

		public string Type {set;get;}

		public string RecordId {set;get;}

		public string Title {set;get;}

		public string Value {set;get;}

		public bool IsDeleted {set;get;}
	}
}
