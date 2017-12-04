namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ProcessNode : SObject
	{
		public string Name {set;get;}
		public string DeveloperName {set;get;}
		public string ProcessDefinitionId {set;get;}
		public ProcessDefinition ProcessDefinition {set;get;}
		public string Description {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
