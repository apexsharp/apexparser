namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SecureAgent : SObject
	{
		public bool IsDeleted {set;get;}

		public string DeveloperName {set;get;}

		public string Language {set;get;}

		public string MasterLabel {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string AgentKey {set;get;}

		public string ProxyUserId {set;get;}

		public User ProxyUser {set;get;}

		public string SecureAgentsClusterId {set;get;}

		public SecureAgentsCluster SecureAgentsCluster {set;get;}

		public int Priority {set;get;}
	}
}
