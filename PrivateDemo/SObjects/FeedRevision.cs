namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FeedRevision : SObject
	{
		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}

		public string FeedEntityId {set;get;}

		public int Revision {set;get;}

		public string Action {set;get;}

		public string EditedAttribute {set;get;}

		public string Value {set;get;}

		public bool IsValueRichText {set;get;}
	}
}
