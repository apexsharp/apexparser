namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PicklistValueInfo : SObject
	{
		public string DurableId {set;get;}
		public string Value {set;get;}
		public string Label {set;get;}
		public bool IsDefaultValue {set;get;}
		public bool IsActive {set;get;}
		public string ValidFor {set;get;}
		public string EntityParticleId {set;get;}
	}
}
