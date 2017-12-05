namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EmailServicesFunction : SObject
	{
		public bool IsActive {set;get;}

		public string FunctionName {set;get;}

		public string AuthorizedSenders {set;get;}

		public bool IsAuthenticationRequired {set;get;}

		public bool IsTlsRequired {set;get;}

		public string AttachmentOption {set;get;}

		public string ApexClassId {set;get;}

		public string OverLimitAction {set;get;}

		public string FunctionInactiveAction {set;get;}

		public string AddressInactiveAction {set;get;}

		public string AuthenticationFailureAction {set;get;}

		public string AuthorizationFailureAction {set;get;}

		public bool IsErrorRoutingEnabled {set;get;}

		public string ErrorRoutingAddress {set;get;}

		public bool IsTextAttachmentsAsBinary {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
