using SalesForceAPI.ApexApi;

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

        public static List<ID> FindSimilar(SObject idea)
        {
            throw new global::System.NotImplementedException("Ideas.FindSimilar");
        }

        public static List<ID> GetAllRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetAllRecentReplies");
        }

        public static List<ID> GetReadRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetReadRecentReplies");
        }

        public static List<ID> GetUnreadRecentReplies(string userId, string communityId)
        {
            throw new global::System.NotImplementedException("Ideas.GetUnreadRecentReplies");
        }

        public static void MarkRead(string ideaId)
        {
            throw new global::System.NotImplementedException("Ideas.MarkRead");
        }
    }
}