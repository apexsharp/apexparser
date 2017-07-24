using Apex.System;

namespace Apex.ConnectApi
{
    public class Topics
    {
        public static Topic AssignTopic(string communityId, string recordId, string topicId)
        {
            throw new global::System.NotImplementedException("Topics.AssignTopic");
        }

        public static Topic AssignTopicByName(string communityId, string recordId, string topicName)
        {
            throw new global::System.NotImplementedException("Topics.AssignTopicByName");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Topics.Clone");
        }

        public static Topic CreateTopic(string communityId, string name, string description)
        {
            throw new global::System.NotImplementedException("Topics.CreateTopic");
        }

        public static void DeleteTopic(string communityId, string topicId)
        {
            throw new global::System.NotImplementedException("Topics.DeleteTopic");
        }

        //public static ChatterGroupSummaryPage GetGroupsRecentlyTalkingAboutTopic(string communityId,string topicId){throw new global::System.NotImplementedException("Topics.GetGroupsRecentlyTalkingAboutTopic");}
        public static TopicPage GetRecentlyTalkingAboutTopicsForGroup(string communityId, string groupId)
        {
            throw new global::System.NotImplementedException("Topics.GetRecentlyTalkingAboutTopicsForGroup");
        }

        public static TopicPage GetRecentlyTalkingAboutTopicsForUser(string communityId, string userId)
        {
            throw new global::System.NotImplementedException("Topics.GetRecentlyTalkingAboutTopicsForUser");
        }

        public static TopicPage GetRelatedTopics(string communityId, string topicId)
        {
            throw new global::System.NotImplementedException("Topics.GetRelatedTopics");
        }

        public static Topic GetTopic(string communityId, string topicId)
        {
            throw new global::System.NotImplementedException("Topics.GetTopic");
        }

        public static TopicSuggestionPage GetTopicSuggestions(string communityId, string recordId)
        {
            throw new global::System.NotImplementedException("Topics.GetTopicSuggestions");
        }

        public static TopicSuggestionPage GetTopicSuggestions(string communityId, string recordId, int maxResults)
        {
            throw new global::System.NotImplementedException("Topics.GetTopicSuggestions");
        }

        public static TopicSuggestionPage GetTopicSuggestionsForText(string communityId, string text)
        {
            throw new global::System.NotImplementedException("Topics.GetTopicSuggestionsForText");
        }

        public static TopicSuggestionPage GetTopicSuggestionsForText(string communityId, string text, int maxResults)
        {
            throw new global::System.NotImplementedException("Topics.GetTopicSuggestionsForText");
        }

        public static TopicPage GetTopics(string communityId)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, TopicSort sortParam)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, int pageParam, int pageSize, TopicSort sortParam)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, string q, bool exactMatch)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, string q, TopicSort sortParam)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, string q, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, string q, int pageParam, int pageSize,
            TopicSort sortParam)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopics(string communityId, string recordId)
        {
            throw new global::System.NotImplementedException("Topics.GetTopics");
        }

        public static TopicPage GetTopicsOrFallBackToRenamedTopics(string communityId, string q, bool exactMatch,
            bool fallBackToRenamedTopics)
        {
            throw new global::System.NotImplementedException("Topics.GetTopicsOrFallBackToRenamedTopics");
        }

        public static TopicPage GetTrendingTopics(string communityId)
        {
            throw new global::System.NotImplementedException("Topics.GetTrendingTopics");
        }

        public static TopicPage GetTrendingTopics(string communityId, int maxResults)
        {
            throw new global::System.NotImplementedException("Topics.GetTrendingTopics");
        }

        public static Topic MergeTopics(string communityId, string topicId, List<string> idsToMerge)
        {
            throw new global::System.NotImplementedException("Topics.MergeTopics");
        }

        public static TopicPage ReassignTopicsByName(string communityId, string recordId, TopicNamesInput topicNames)
        {
            throw new global::System.NotImplementedException("Topics.ReassignTopicsByName");
        }

        //public static void SetTestGetGroupsRecentlyTalkingAboutTopic(string communityId,string topicId,ChatterGroupSummaryPage result){throw new global::System.NotImplementedException("Topics.SetTestGetGroupsRecentlyTalkingAboutTopic");}
        public static void SetTestGetRecentlyTalkingAboutTopicsForGroup(string communityId, string groupId,
            TopicPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetRecentlyTalkingAboutTopicsForGroup");
        }

        public static void SetTestGetRecentlyTalkingAboutTopicsForUser(string communityId, string userId,
            TopicPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetRecentlyTalkingAboutTopicsForUser");
        }

        public static void SetTestGetRelatedTopics(string communityId, string topicId, TopicPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetRelatedTopics");
        }

        public static void SetTestGetTopicSuggestions(string communityId, string recordId, TopicSuggestionPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTopicSuggestions");
        }

        public static void SetTestGetTopicSuggestions(string communityId, string recordId, int maxResults,
            TopicSuggestionPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTopicSuggestions");
        }

        public static void SetTestGetTopicSuggestionsForText(string communityId, string text,
            TopicSuggestionPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTopicSuggestionsForText");
        }

        public static void SetTestGetTopicSuggestionsForText(string communityId, string text, int maxResults,
            TopicSuggestionPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTopicSuggestionsForText");
        }

        public static void SetTestGetTrendingTopics(string communityId, TopicPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTrendingTopics");
        }

        public static void SetTestGetTrendingTopics(string communityId, int maxResults, TopicPage result)
        {
            throw new global::System.NotImplementedException("Topics.SetTestGetTrendingTopics");
        }

        public static void UnassignTopic(string communityId, string recordId, string topicId)
        {
            throw new global::System.NotImplementedException("Topics.UnassignTopic");
        }

        public static Topic UpdateTopic(string communityId, string topicId, TopicInput topic)
        {
            throw new global::System.NotImplementedException("Topics.UpdateTopic");
        }
    }
}