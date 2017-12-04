namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class LoginIp : SObject
	{
		public string UsersId {set;get;}
		public User Users {set;get;}
		public string SourceIp {set;get;}
		public DateTime CreatedDate {set;get;}
		public bool IsAuthenticated {set;get;}
		public DateTime ChallengeSentDate {set;get;}
		public string ChallengeMethod {set;get;}
	}
}
