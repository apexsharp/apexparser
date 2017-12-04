namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OutgoingEmailRelation : SObject
	{
		public string ExternalId {set;get;}
		public string OutgoingEmailId {set;get;}
		public string RelationId {set;get;}
		public Contact Relation {set;get;}
		public string RelationAddress {set;get;}
	}
}
