namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentWorkspaceMember : SObject
	{
		public string ContentWorkspaceId {set;get;}

		public ContentWorkspace ContentWorkspace {set;get;}

		public string ContentWorkspacePermissionId {set;get;}

		public ContentWorkspacePermission ContentWorkspacePermission {set;get;}

		public string MemberId {set;get;}

		public Group Member {set;get;}

		public string MemberType {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}
	}
}
