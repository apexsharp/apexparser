using Apex.System;

namespace Apex.ConnectApi
{
    public class ChatterUsers
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("ChatterUsers.Clone");
        }

        public static void DeletePhoto(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.DeletePhoto");
        }

        public static Subscription Follow(string communityId, string userId, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.Follow");
        }

        public static UserChatterSettings GetChatterSettings(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetChatterSettings");
        }

        public static FollowerPage GetFollowers(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowers");
        }

        public static FollowerPage GetFollowers(string communityId, string userId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowers");
        }

        public static FollowingPage GetFollowings(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string userId, int pageParam)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string userId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string userId, string filterType)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string userId, string filterType, int pageParam)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static FollowingPage GetFollowings(string communityId, string userId, string filterType, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetFollowings");
        }

        public static UserGroupPage GetGroups(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetGroups");
        }

        public static UserGroupPage GetGroups(string communityId, string userId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetGroups");
        }

        public static Photo GetPhoto(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetPhoto");
        }

        public static Reputation GetReputation(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetReputation");
        }

        public static UserDetail GetUser(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetUser");
        }

        public static List<BatchResult> GetUserBatch(string communityId, List<string> userIds)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetUserBatch");
        }

        public static UserPage GetUsers(string communityId)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetUsers");
        }

        public static UserPage GetUsers(string communityId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.GetUsers");
        }

        public static UserGroupPage SearchUserGroups(string communityId, string userId, string q)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SearchUserGroups");
        }

        public static UserGroupPage SearchUserGroups(string communityId, string userId, string q, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SearchUserGroups");
        }

        public static UserPage SearchUsers(string communityId, string q)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SearchUsers");
        }

        public static UserPage SearchUsers(string communityId, string q, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SearchUsers");
        }

        public static UserPage SearchUsers(string communityId, string q, string searchContextId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SearchUsers");
        }

        public static Photo SetPhoto(string communityId, string userId, BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetPhoto");
        }

        public static Photo SetPhoto(string communityId, string userId, string fileId, int versionNumber)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetPhoto");
        }

        public static Photo SetPhotoWithAttributes(string communityId, string userId, PhotoInput photo)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetPhotoWithAttributes");
        }

        public static Photo SetPhotoWithAttributes(string communityId, string userId, PhotoInput photo,
            BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetPhotoWithAttributes");
        }

        public static void SetTestSearchUsers(string communityId, string q, UserPage result)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetTestSearchUsers");
        }

        public static void SetTestSearchUsers(string communityId, string q, int pageParam, int pageSize,
            UserPage result)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetTestSearchUsers");
        }

        public static void SetTestSearchUsers(string communityId, string q, string searchContextId, int pageParam,
            int pageSize, UserPage result)
        {
            throw new global::System.NotImplementedException("ChatterUsers.SetTestSearchUsers");
        }

        public static UserChatterSettings UpdateChatterSettings(string communityId, string userId,
            GroupEmailFrequency defaultGroupEmailFrequency)
        {
            throw new global::System.NotImplementedException("ChatterUsers.UpdateChatterSettings");
        }

        public static UserDetail UpdateUser(string communityId, string userId, UserInput userInput)
        {
            throw new global::System.NotImplementedException("ChatterUsers.UpdateUser");
        }
    }
}