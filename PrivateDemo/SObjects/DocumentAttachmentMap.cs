namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DocumentAttachmentMap : SObject
	{
		public string ParentId {set;get;}

		public string DocumentId {set;get;}

		public int DocumentSequence {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}
	}
}
