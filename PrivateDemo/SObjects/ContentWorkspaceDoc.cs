namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentWorkspaceDoc : SObject
	{
		public string ContentWorkspaceId {set;get;}

		public ContentWorkspace ContentWorkspace {set;get;}

		public string ContentDocumentId {set;get;}

		public ContentDocument ContentDocument {set;get;}

		public DateTime CreatedDate {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsOwner {set;get;}

		public bool IsDeleted {set;get;}
	}
}
