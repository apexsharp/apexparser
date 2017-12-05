namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class RelationshipInfo : SObject
	{
		public string DurableId {set;get;}

		public string ChildSobjectId {set;get;}

		public string FieldId {set;get;}

		public bool IsCascadeDelete {set;get;}

		public bool IsDeprecatedAndHidden {set;get;}

		public bool IsRestrictedDelete {set;get;}

		public string JunctionIdListNames {set;get;}
	}
}
