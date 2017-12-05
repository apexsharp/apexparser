namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailCapture : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsActive {set;get;}

		public string ToPattern {set;get;}

		public string FromPattern {set;get;}

		public string Sender {set;get;}

		public string Recipient {set;get;}

		public DateTime CaptureDate {set;get;}

		public int RawMessageLength {set;get;}

		public string RawMessage {set;get;}
	}
}
