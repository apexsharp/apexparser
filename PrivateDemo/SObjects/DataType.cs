namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DataType : SObject
	{
		public string DurableId {set;get;}

		public string DeveloperName {set;get;}

		public bool IsComplex {set;get;}

		public string ContextServiceDataTypeId {set;get;}

		public string ContextWsdlDataTypeId {set;get;}
	}
}
