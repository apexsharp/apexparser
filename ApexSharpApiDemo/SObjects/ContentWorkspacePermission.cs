namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentWorkspacePermission : SObject
	{
		public string Name {set;get;}
		public string Type {set;get;}
		public bool PermissionsManageWorkspace {set;get;}
		public bool PermissionsAddContent {set;get;}
		public bool PermissionsAddContentOBO {set;get;}
		public bool PermissionsArchiveContent {set;get;}
		public bool PermissionsDeleteContent {set;get;}
		public bool PermissionsFeatureContent {set;get;}
		public bool PermissionsViewComments {set;get;}
		public bool PermissionsAddComment {set;get;}
		public bool PermissionsModifyComments {set;get;}
		public bool PermissionsTagContent {set;get;}
		public bool PermissionsDeliverContent {set;get;}
		public bool PermissionsChatterSharing {set;get;}
		public bool PermissionsOrganizeFileAndFolder {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public string Description {set;get;}
	}
}
