namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Dashboard : SObject
	{
		public bool IsDeleted {set;get;}

		public string FolderId {set;get;}

		public Folder Folder {set;get;}

		public string FolderName {set;get;}

		public string Title {set;get;}

		public string DeveloperName {set;get;}

		public string NamespacePrefix {set;get;}

		public string Description {set;get;}

		public string LeftSize {set;get;}

		public string MiddleSize {set;get;}

		public string RightSize {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string RunningUserId {set;get;}

		public User RunningUser {set;get;}

		public int TitleColor {set;get;}

		public int TitleSize {set;get;}

		public int TextColor {set;get;}

		public int BackgroundStart {set;get;}

		public int BackgroundEnd {set;get;}

		public string BackgroundDirection {set;get;}

		public string Type {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string DashboardResultRefreshedDate {set;get;}

		public string DashboardResultRunningUser {set;get;}
	}
}
