namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Pricebook2History : SObject
	{
		public bool IsDeleted {set;get;}

		public string Pricebook2Id {set;get;}

		public Pricebook2 Pricebook2 {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string Field {set;get;}

		public object OldValue {set;get;}

		public object NewValue {set;get;}
	}
}
