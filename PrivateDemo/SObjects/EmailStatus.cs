namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailStatus : SObject
	{
		public string TaskId {set;get;}

		public Task Task {set;get;}

		public string WhoId {set;get;}

		public Contact Who {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public int TimesOpened {set;get;}

		public DateTime FirstOpenDate {set;get;}

		public DateTime LastOpenDate {set;get;}

		public string EmailTemplateName {set;get;}
	}
}
