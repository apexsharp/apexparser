namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ProcessInstanceNode : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string ProcessInstanceId {set;get;}
		public ProcessInstance ProcessInstance {set;get;}
		public string ProcessNodeId {set;get;}
		public ProcessNode ProcessNode {set;get;}
		public string NodeStatus {set;get;}
		public DateTime CompletedDate {set;get;}
		public string LastActorId {set;get;}
		public User LastActor {set;get;}
		public string ProcessNodeName {set;get;}
		public double ElapsedTimeInDays {set;get;}
		public double ElapsedTimeInHours {set;get;}
		public double ElapsedTimeInMinutes {set;get;}
	}
}
