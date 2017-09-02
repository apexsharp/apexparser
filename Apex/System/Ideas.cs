using SalesForceAPI.Apex;

namespace Apex.System
{
    public class Ideas
    {
        public Ideas()
        {
            throw new global::System.NotImplementedException("Ideas");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Ideas.Clone");
        }

        public static List<Id> FindSimilar(SObject idea)
        {
            throw new global::System.NotImplementedException("Ideas.FindSimilar");
        }

        public static List<Id> GetAllRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetAllRecentReplies");
        }

        public static List<Id> GetReadRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetReadRecentReplies");
        }

        public static List<Id> GetUnreadRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetUnreadRecentReplies");
        }

        public static void MarkRead(string ideaId)
        {
            throw new global::System.NotImplementedException("Ideas.MarkRead");
        }
    }
}