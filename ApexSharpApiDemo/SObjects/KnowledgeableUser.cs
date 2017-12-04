namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class KnowledgeableUser : SObject
	{
		public string UserId {set;get;}
		public string TopicId {set;get;}
		public int RawRank {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
