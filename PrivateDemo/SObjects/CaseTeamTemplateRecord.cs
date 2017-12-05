namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseTeamTemplateRecord : SObject
	{
		public string ParentId {set;get;}

		public Case Parent {set;get;}

		public string TeamTemplateId {set;get;}

		public CaseTeamTemplate TeamTemplate {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
