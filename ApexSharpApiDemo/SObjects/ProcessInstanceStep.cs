namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ProcessInstanceStep : SObject
	{
		public string ProcessInstanceId {set;get;}
		public ProcessInstance ProcessInstance {set;get;}
		public string StepStatus {set;get;}
		public string OriginalActorId {set;get;}
		public Group OriginalActor {set;get;}
		public string ActorId {set;get;}
		public Group Actor {set;get;}
		public string Comments {set;get;}
		public string StepNodeId {set;get;}
		public double ElapsedTimeInDays {set;get;}
		public double ElapsedTimeInHours {set;get;}
		public double ElapsedTimeInMinutes {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
