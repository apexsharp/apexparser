namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SecureAgentPluginProperty : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string SecureAgentPluginId {set;get;}

		public SecureAgentPlugin SecureAgentPlugin {set;get;}

		public string PropertyName {set;get;}

		public string PropertyValue {set;get;}
	}
}
