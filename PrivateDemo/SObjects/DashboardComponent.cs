namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DashboardComponent : SObject
	{
		public string Name {set;get;}

		public string DashboardId {set;get;}

		public Dashboard Dashboard {set;get;}

		public string CustomReportId {set;get;}
	}
}
