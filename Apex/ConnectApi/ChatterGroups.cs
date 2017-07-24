using Apex.System;

namespace Apex.ConnectApi
{
    public class ChatterGroups
    {
        public static GroupMember AddMember(string communityId, string groupId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.AddMember");
        }

        public static GroupMember AddMemberWithRole(string communityId, string groupId, string userId,
            GroupMembershipType role)
        {
            throw new global::System.NotImplementedException("ChatterGroups.AddMemberWithRole");
        }

        public static GroupRecord AddRecord(string communityId, string groupId, string recordId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.AddRecord");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("ChatterGroups.Clone");
        }

        public static ChatterGroupDetail CreateGroup(string communityId, ChatterGroupInput groupInput)
        {
            throw new global::System.NotImplementedException("ChatterGroups.CreateGroup");
        }

        public static void DeleteBannerPhoto(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.DeleteBannerPhoto");
        }

        public static void DeleteGroup(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.DeleteGroup");
        }

        public static void DeleteMember(string communityId, string membershipId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.DeleteMember");
        }

        public static void DeletePhoto(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.DeletePhoto");
        }

        public static Subscription Follow(string communityId, string groupId, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.Follow");
        }

        public static AnnouncementPage GetAnnouncements(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetAnnouncements");
        }

        public static AnnouncementPage GetAnnouncements(string communityId, string groupId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetAnnouncements");
        }

        public static BannerPhoto GetBannerPhoto(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetBannerPhoto");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId, int pageParam)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId, string filterType)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId, string filterType, int pageParam)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string groupId, string filterType, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetFollowings");
        }

        public static ChatterGroupDetail GetGroup(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroup");
        }

        public static List<BatchResult> GetGroupBatch(string communityId, List<string> groupIds)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroupBatch");
        }

        public static GroupMembershipRequest GetGroupMembershipRequest(string communityId, string requestId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroupMembershipRequest");
        }

        public static GroupMembershipRequests GetGroupMembershipRequests(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroupMembershipRequests");
        }

        public static GroupMembershipRequests GetGroupMembershipRequests(string communityId, string groupId,
            GroupMembershipRequestStatus status)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroupMembershipRequests");
        }

        public static ChatterGroupPage GetGroups(string communityId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroups");
        }

        public static ChatterGroupPage GetGroups(string communityId, GroupArchiveStatus archiveStatus, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroups");
        }

        public static ChatterGroupPage GetGroups(string communityId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetGroups");
        }

        public static GroupMember GetMember(string communityId, string membershipId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetMember");
        }

        public static GroupMemberPage GetMembers(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetMembers");
        }

        public static GroupMemberPage GetMembers(string communityId, string groupId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetMembers");
        }

        public static List<BatchResult> GetMembershipBatch(string communityId, List<string> membershipIds)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetMembershipBatch");
        }

        public static GroupChatterSettings GetMyChatterSettings(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetMyChatterSettings");
        }

        public static Photo GetPhoto(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetPhoto");
        }

        public static GroupRecord GetRecord(string communityId, string groupRecordId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetRecord");
        }

        public static GroupRecordPage GetRecords(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetRecords");
        }

        public static GroupRecordPage GetRecords(string communityId, string groupId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.GetRecords");
        }

        public static Invitations InviteUsers(string groupId, InviteInput invite)
        {
            throw new global::System.NotImplementedException("ChatterGroups.InviteUsers");
        }

        public static Announcement PostAnnouncement(string communityId, string groupId, AnnouncementInput announcement)
        {
            throw new global::System.NotImplementedException("ChatterGroups.PostAnnouncement");
        }

        public static void RemoveRecord(string communityId, string groupRecordId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.RemoveRecord");
        }

        public static GroupMembershipRequest RequestGroupMembership(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("ChatterGroups.RequestGroupMembership");
        }

        public static ChatterGroupPage SearchGroups(string communityId, string q)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SearchGroups");
        }

        public static ChatterGroupPage SearchGroups(string communityId, string q, GroupArchiveStatus archiveStatus,
            int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SearchGroups");
        }

        public static ChatterGroupPage SearchGroups(string communityId, string q, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SearchGroups");
        }

        public static BannerPhoto SetBannerPhoto(string communityId, string groupId, BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetBannerPhoto");
        }

        public static BannerPhoto SetBannerPhoto(string communityId, string groupId, string fileId, int versionNumber)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetBannerPhoto");
        }

        public static BannerPhoto SetBannerPhotoWithAttributes(string communityId, string groupId,
            BannerPhotoInput bannerPhoto)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetBannerPhotoWithAttributes");
        }

        public static BannerPhoto SetBannerPhotoWithAttributes(string communityId, string groupId,
            BannerPhotoInput bannerPhoto, BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetBannerPhotoWithAttributes");
        }

        public static Photo SetPhoto(string communityId, string groupId, BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetPhoto");
        }

        public static Photo SetPhoto(string communityId, string groupId, string fileId, int versionNumber)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetPhoto");
        }

        public static Photo SetPhotoWithAttributes(string communityId, string groupId, PhotoInput photo)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetPhotoWithAttributes");
        }

        public static Photo SetPhotoWithAttributes(string communityId, string groupId, PhotoInput photo,
            BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetPhotoWithAttributes");
        }

        public static void SetTestSearchGroups(string communityId, string q, ChatterGroupPage result)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetTestSearchGroups");
        }

        public static void SetTestSearchGroups(string communityId, string q, GroupArchiveStatus archiveStatus,
            int pageParam, int pageSize, ChatterGroupPage result)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetTestSearchGroups");
        }

        public static void SetTestSearchGroups(string communityId, string q, int pageParam, int pageSize,
            ChatterGroupPage result)
        {
            throw new global::System.NotImplementedException("ChatterGroups.SetTestSearchGroups");
        }

        public static ChatterGroupDetail UpdateGroup(string communityId, string groupId, ChatterGroupInput groupInput)
        {
            throw new global::System.NotImplementedException("ChatterGroups.UpdateGroup");
        }

        public static GroupMember UpdateGroupMember(string communityId, string membershipId, GroupMembershipType role)
        {
            throw new global::System.NotImplementedException("ChatterGroups.UpdateGroupMember");
        }

        public static GroupChatterSettings UpdateMyChatterSettings(string communityId, string groupId,
            GroupEmailFrequency emailFrequency)
        {
            throw new global::System.NotImplementedException("ChatterGroups.UpdateMyChatterSettings");
        }

        public static GroupMembershipRequest UpdateRequestStatus(string communityId, string requestId,
            GroupMembershipRequestStatus status)
        {
            throw new global::System.NotImplementedException("ChatterGroups.UpdateRequestStatus");
        }

        public static GroupMembershipRequest UpdateRequestStatus(string communityId, string requestId,
            GroupMembershipRequestStatus status, string responseMessage)
        {
            throw new global::System.NotImplementedException("ChatterGroups.UpdateRequestStatus");
        }
    }
}