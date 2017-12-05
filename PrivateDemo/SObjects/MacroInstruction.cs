namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class MacroInstruction : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string MacroId {set;get;}

		public Macro Macro {set;get;}

		public string Operation {set;get;}

		public string Target {set;get;}

		public string Value {set;get;}

		public string ValueRecord {set;get;}

		public int SortOrder {set;get;}
	}
}
