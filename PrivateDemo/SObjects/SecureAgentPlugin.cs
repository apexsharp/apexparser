namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SecureAgentPlugin : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string SecureAgentId {set;get;}

		public SecureAgent SecureAgent {set;get;}

		public string PluginName {set;get;}

		public string PluginType {set;get;}

		public string RequestedVersion {set;get;}

		public DateTime UpdateWindowStart {set;get;}

		public DateTime UpdateWindowEnd {set;get;}
	}
}
