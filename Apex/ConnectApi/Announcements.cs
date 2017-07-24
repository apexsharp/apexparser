using Apex.System;

namespace Apex.ConnectApi
{
    public class Announcements
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("Announcements.Clone");
        }

        public static void DeleteAnnouncement(string communityId, string announcementId)
        {
            throw new global::System.NotImplementedException("Announcements.DeleteAnnouncement");
        }

        public static Announcement GetAnnouncement(string communityId, string announcementId)
        {
            throw new global::System.NotImplementedException("Announcements.GetAnnouncement");
        }

        public static AnnouncementPage GetAnnouncements(string communityId, string parentId)
        {
            throw new global::System.NotImplementedException("Announcements.GetAnnouncements");
        }

        public static AnnouncementPage GetAnnouncements(string communityId, string parentId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("Announcements.GetAnnouncements");
        }

        public static Announcement PostAnnouncement(string communityId, AnnouncementInput announcement)
        {
            throw new global::System.NotImplementedException("Announcements.PostAnnouncement");
        }

        public static Announcement UpdateAnnouncement(string communityId, string announcementId,
            DateTime expirationDate)
        {
            throw new global::System.NotImplementedException("Announcements.UpdateAnnouncement");
        }
    }
}