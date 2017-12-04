namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CronJobDetail : SObject
	{
		public string Name {set;get;}
		public string JobType {set;get;}
	}
}
