namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexPage : SObject
	{
		public string NamespacePrefix {set;get;}
		public string Name {set;get;}
		public double ApiVersion {set;get;}
		public string MasterLabel {set;get;}
		public string Description {set;get;}
		public string ControllerType {set;get;}
		public string ControllerKey {set;get;}
		public bool IsAvailableInTouch {set;get;}
		public bool IsConfirmationTokenRequired {set;get;}
		public string Markup {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
