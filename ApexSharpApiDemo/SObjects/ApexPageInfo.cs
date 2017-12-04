namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexPageInfo : SObject
	{
		public string DurableId {set;get;}
		public string ApexPageId {set;get;}
		public string Name {set;get;}
		public string NameSpacePrefix {set;get;}
		public double ApiVersion {set;get;}
		public string Description {set;get;}
		public bool IsAvailableInTouch {set;get;}
		public string MasterLabel {set;get;}
		public string IsShowHeader {set;get;}
	}
}
