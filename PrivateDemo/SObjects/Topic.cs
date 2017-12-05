namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Topic : SObject
	{
		public string Name {set;get;}

		public string Description {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public int TalkingAbout {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
