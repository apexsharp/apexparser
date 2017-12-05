namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentDocumentLink : SObject
	{
		public string LinkedEntityId {set;get;}

		public Account LinkedEntity {set;get;}

		public string ContentDocumentId {set;get;}

		public ContentDocument ContentDocument {set;get;}

		public bool IsDeleted {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ShareType {set;get;}

		public string Visibility {set;get;}
	}
}
