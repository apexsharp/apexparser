namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AuthSession : SObject
	{
		public string UsersId {set;get;}
		public User Users {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public int NumSecondsValid {set;get;}
		public string UserType {set;get;}
		public string SourceIp {set;get;}
		public string LoginType {set;get;}
		public string SessionType {set;get;}
		public string SessionSecurityLevel {set;get;}
		public string LogoutUrl {set;get;}
		public string ParentId {set;get;}
		public string LoginHistoryId {set;get;}
		public LoginHistory LoginHistory {set;get;}
		public string LoginGeoId {set;get;}
		public LoginGeo LoginGeo {set;get;}
		public bool IsCurrent {set;get;}
	}
}
