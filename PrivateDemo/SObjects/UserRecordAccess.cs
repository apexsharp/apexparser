namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserRecordAccess : SObject
	{
		public string UserId {set;get;}

		public string RecordId {set;get;}

		public bool HasReadAccess {set;get;}

		public bool HasEditAccess {set;get;}

		public bool HasDeleteAccess {set;get;}

		public bool HasTransferAccess {set;get;}

		public bool HasAllAccess {set;get;}

		public string MaxAccessLevel {set;get;}
	}
}
