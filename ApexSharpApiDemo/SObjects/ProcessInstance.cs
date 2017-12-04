namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ProcessInstance : SObject
	{
		public string ProcessDefinitionId {set;get;}
		public ProcessDefinition ProcessDefinition {set;get;}
		public string TargetObjectId {set;get;}
		public Account TargetObject {set;get;}
		public string Status {set;get;}
		public DateTime CompletedDate {set;get;}
		public string LastActorId {set;get;}
		public User LastActor {set;get;}
		public double ElapsedTimeInDays {set;get;}
		public double ElapsedTimeInHours {set;get;}
		public double ElapsedTimeInMinutes {set;get;}
		public string SubmittedById {set;get;}
		public User SubmittedBy {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
