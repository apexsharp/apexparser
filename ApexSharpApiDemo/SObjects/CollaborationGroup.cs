namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CollaborationGroup : SObject
	{
		public string Name {set;get;}
		public int MemberCount {set;get;}
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public string CollaborationType {set;get;}
		public string Description {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string FullPhotoUrl {set;get;}
		public string MediumPhotoUrl {set;get;}
		public string SmallPhotoUrl {set;get;}
		public DateTime LastFeedModifiedDate {set;get;}
		public string InformationTitle {set;get;}
		public string InformationBody {set;get;}
		public bool HasPrivateFieldsAccess {set;get;}
		public bool CanHaveGuests {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
		public bool IsArchived {set;get;}
		public bool IsAutoArchiveDisabled {set;get;}
		public string AnnouncementId {set;get;}
		public Announcement Announcement {set;get;}
		public string GroupEmail {set;get;}
		public string BannerPhotoUrl {set;get;}
		public bool IsBroadcast {set;get;}
	}
}
