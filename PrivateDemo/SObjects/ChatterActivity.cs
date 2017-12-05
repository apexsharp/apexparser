namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ChatterActivity : SObject
	{
		public string ParentId {set;get;}

		public int PostCount {set;get;}

		public int CommentCount {set;get;}

		public int CommentReceivedCount {set;get;}

		public int LikeReceivedCount {set;get;}

		public int InfluenceRawRank {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
