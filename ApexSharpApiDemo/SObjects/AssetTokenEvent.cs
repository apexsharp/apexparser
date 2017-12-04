namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AssetTokenEvent : SObject
	{
		public string ReplayId {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public string ConnectedAppId {set;get;}
		public ConnectedApplication ConnectedApp {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string AssetId {set;get;}
		public Asset Asset {set;get;}
		public string Name {set;get;}
		public string DeviceId {set;get;}
		public string DeviceKey {set;get;}
		public DateTime Expiration {set;get;}
		public string AssetSerialNumber {set;get;}
		public string AssetName {set;get;}
		public string ActorTokenPayload {set;get;}
	}
}
