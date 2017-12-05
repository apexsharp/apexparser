namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ProcessDefinition : SObject
	{
		public string Name {set;get;}

		public string DeveloperName {set;get;}

		public string Type {set;get;}

		public string Description {set;get;}

		public string TableEnumOrId {set;get;}

		public string LockType {set;get;}

		public string State {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
