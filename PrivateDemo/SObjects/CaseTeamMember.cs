namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseTeamMember : SObject
	{
		public string ParentId {set;get;}

		public Case Parent {set;get;}

		public string MemberId {set;get;}

		public Contact Member {set;get;}

		public string TeamTemplateMemberId {set;get;}

		public CaseTeamTemplateMember TeamTemplateMember {set;get;}

		public string TeamRoleId {set;get;}

		public CaseTeamRole TeamRole {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
