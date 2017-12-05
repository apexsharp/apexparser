namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Attachment : SObject
	{
		public bool IsDeleted {set;get;}

		public string ParentId {set;get;}

		public Account Parent {set;get;}

		public string Name {set;get;}

		public bool IsPrivate {set;get;}

		public string ContentType {set;get;}

		public int BodyLength {set;get;}

		public string Body {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string Description {set;get;}
	}
}
