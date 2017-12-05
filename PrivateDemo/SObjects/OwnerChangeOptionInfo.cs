namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OwnerChangeOptionInfo : SObject
	{
		public string DurableId {set;get;}

		public string EntityDefinitionId {set;get;}

		public string Name {set;get;}

		public string Label {set;get;}

		public bool IsEditable {set;get;}

		public bool DefaultValue {set;get;}
	}
}
