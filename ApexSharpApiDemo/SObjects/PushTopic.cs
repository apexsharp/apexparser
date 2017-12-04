namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PushTopic : SObject
	{
		public string Name {set;get;}
		public string Query {set;get;}
		public double ApiVersion {set;get;}
		public bool IsActive {set;get;}
		public string NotifyForFields {set;get;}
		public string NotifyForOperations {set;get;}
		public string Description {set;get;}
		public bool NotifyForOperationCreate {set;get;}
		public bool NotifyForOperationUpdate {set;get;}
		public bool NotifyForOperationDelete {set;get;}
		public bool NotifyForOperationUndelete {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
