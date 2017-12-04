namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserProvisioningRequest : SObject
	{
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string SalesforceUserId {set;get;}
		public User SalesforceUser {set;get;}
		public string ExternalUserId {set;get;}
		public string AppName {set;get;}
		public string State {set;get;}
		public string Operation {set;get;}
		public DateTime ScheduleDate {set;get;}
		public string ConnectedAppId {set;get;}
		public ConnectedApplication ConnectedApp {set;get;}
		public string UserProvConfigId {set;get;}
		public UserProvisioningConfig UserProvConfig {set;get;}
		public string UserProvAccountId {set;get;}
		public UserProvAccount UserProvAccount {set;get;}
		public string ApprovalStatus {set;get;}
		public string ManagerId {set;get;}
		public User Manager {set;get;}
		public int RetryCount {set;get;}
		public string ParentId {set;get;}
		public UserProvisioningRequest Parent {set;get;}
	}
}
