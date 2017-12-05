namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentFolderLink : SObject
	{
		public string ParentEntityId {set;get;}

		public string ContentFolderId {set;get;}

		public ContentFolder ContentFolder {set;get;}

		public bool IsDeleted {set;get;}

		public string EnableFolderStatus {set;get;}
	}
}
