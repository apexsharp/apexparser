namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AuraDefinition : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string AuraDefinitionBundleId {set;get;}
		public AuraDefinitionBundle AuraDefinitionBundle {set;get;}
		public string DefType {set;get;}
		public string Format {set;get;}
		public string Source {set;get;}
	}
}
