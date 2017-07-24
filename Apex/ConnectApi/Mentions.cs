using Apex.System;

namespace Apex.ConnectApi
{
    public class Mentions
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("Mentions.Clone");
        }

        public static MentionCompletionPage GetMentionCompletions(string communityId, string q, string contextId)
        {
            throw new global::System.NotImplementedException("Mentions.GetMentionCompletions");
        }

        public static MentionCompletionPage GetMentionCompletions(string communityId, string q, string contextId,
            MentionCompletionType type, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("Mentions.GetMentionCompletions");
        }

        public static MentionValidations GetMentionValidations(string communityId, string parentId,
            List<string> recordIds, FeedItemVisibilityType visibility)
        {
            throw new global::System.NotImplementedException("Mentions.GetMentionValidations");
        }

        public static void SetTestGetMentionCompletions(string communityId, string q, string contextId,
            MentionCompletionPage result)
        {
            throw new global::System.NotImplementedException("Mentions.SetTestGetMentionCompletions");
        }

        public static void SetTestGetMentionCompletions(string communityId, string q, string contextId,
            MentionCompletionType type, int pageParam, int pageSize, MentionCompletionPage result)
        {
            throw new global::System.NotImplementedException("Mentions.SetTestGetMentionCompletions");
        }
    }
}