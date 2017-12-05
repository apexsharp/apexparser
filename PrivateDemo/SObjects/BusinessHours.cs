namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class BusinessHours : SObject
	{
		public string Name {set;get;}

		public bool IsActive {set;get;}

		public bool IsDefault {set;get;}

		public DateTime SundayStartTime {set;get;}

		public DateTime SundayEndTime {set;get;}

		public DateTime MondayStartTime {set;get;}

		public DateTime MondayEndTime {set;get;}

		public DateTime TuesdayStartTime {set;get;}

		public DateTime TuesdayEndTime {set;get;}

		public DateTime WednesdayStartTime {set;get;}

		public DateTime WednesdayEndTime {set;get;}

		public DateTime ThursdayStartTime {set;get;}

		public DateTime ThursdayEndTime {set;get;}

		public DateTime FridayStartTime {set;get;}

		public DateTime FridayEndTime {set;get;}

		public DateTime SaturdayStartTime {set;get;}

		public DateTime SaturdayEndTime {set;get;}

		public string TimeZoneSidKey {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastViewedDate {set;get;}
	}
}
