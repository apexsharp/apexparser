using Apex.System;

namespace Apex.ConnectApi
{
    public class ChatterFeeds
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("ChatterFeeds.Clone");
        }

        public static ChatterStream CreateStream(string communityId, ChatterStreamInput streamInput)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.CreateStream");
        }

        public static void DeleteComment(string communityId, string commentId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.DeleteComment");
        }

        public static void DeleteFeedElement(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.DeleteFeedElement");
        }

        public static void DeleteFeedItem(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.DeleteFeedItem");
        }

        public static void DeleteLike(string communityId, string likeId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.DeleteLike");
        }

        public static void DeleteStream(string communityId, string streamId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.DeleteStream");
        }

        public static Comment GetComment(string communityId, string commentId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetComment");
        }

        public static CommentPage GetCommentsForFeedElement(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetCommentsForFeedElement");
        }

        public static CommentPage GetCommentsForFeedElement(string communityId, string feedElementId, string pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetCommentsForFeedElement");
        }

        public static CommentPage GetCommentsForFeedItem(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetCommentsForFeedItem");
        }

        public static CommentPage GetCommentsForFeedItem(string communityId, string feedItemId, string pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetCommentsForFeedItem");
        }

        public static Feed GetFeed(string communityId, FeedType feedType)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeed");
        }

        public static Feed GetFeed(string communityId, FeedType feedType, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeed");
        }

        public static Feed GetFeed(string communityId, FeedType feedType, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeed");
        }

        public static Feed GetFeed(string communityId, FeedType feedType, string subjectId, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeed");
        }

        public static FeedDirectory GetFeedDirectory(string communityId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedDirectory");
        }

        public static FeedElement GetFeedElement(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElement");
        }

        public static FeedElement GetFeedElement(string communityId, string feedElementId, int recentCommentCount,
            int elementsPerBundle)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElement");
        }

        public static List<BatchResult> GetFeedElementBatch(string communityId, List<string> feedElementIds)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementBatch");
        }

        public static PollCapability GetFeedElementPoll(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementPoll");
        }

        public static FeedElementPage GetFeedElementsFromBundle(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromBundle");
        }

        public static FeedElementPage GetFeedElementsFromBundle(string communityId, string feedElementId,
            string pageParam, int pageSize, int elementsPerBundle, int recentCommentCount)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromBundle");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, bool showInternalOnly, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFeed");
        }

        public static FeedElementPage GetFeedElementsFromFilterFeed(string communityId, string subjectId,
            string keyPrefix)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFilterFeed");
        }

        public static FeedElementPage GetFeedElementsFromFilterFeed(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFilterFeed");
        }

        public static FeedElementPage GetFeedElementsFromFilterFeed(string communityId, string subjectId,
            string keyPrefix, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFilterFeed");
        }

        public static FeedElementPage GetFeedElementsFromFilterFeedUpdatedSince(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsFromFilterFeedUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            string subjectId, int recentCommentCount, FeedDensity density, string pageParam, int pageSize,
            string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            string subjectId, int recentCommentCount, FeedDensity density, string pageParam, int pageSize,
            string updatedSince, bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            string subjectId, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, string updatedSince, bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            string subjectId, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, string updatedSince, bool showInternalOnly, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedElementPage GetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            string subjectId, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, string updatedSince, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedElementsUpdatedSince");
        }

        public static FeedItem GetFeedItem(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItem");
        }

        public static List<BatchResult> GetFeedItemBatch(string communityId, List<string> feedItemIds)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemBatch");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFeed");
        }

        public static FeedItemPage GetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFilterFeed");
        }

        public static FeedItemPage GetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFilterFeed");
        }

        public static FeedItemPage GetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFilterFeed");
        }

        public static FeedItemPage GetFeedItemsFromFilterFeedUpdatedSince(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, FeedDensity density, string pageParam, int pageSize,
            string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsFromFilterFeedUpdatedSince");
        }

        public static FeedItemPage GetFeedItemsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsUpdatedSince");
        }

        public static FeedItemPage GetFeedItemsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsUpdatedSince");
        }

        public static FeedItemPage GetFeedItemsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedItemsUpdatedSince");
        }

        public static FeedPoll GetFeedPoll(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFeedPoll");
        }

        public static Feed GetFilterFeed(string communityId, string subjectId, string keyPrefix)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFilterFeed");
        }

        public static Feed GetFilterFeed(string communityId, string subjectId, string keyPrefix,
            FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFilterFeed");
        }

        public static FeedDirectory GetFilterFeedDirectory(string communityId, string subjectId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetFilterFeedDirectory");
        }

        public static ChatterLike GetLike(string communityId, string likeId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLike");
        }

        public static ChatterLikePage GetLikesForComment(string communityId, string commentId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForComment");
        }

        public static ChatterLikePage GetLikesForComment(string communityId, string commentId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForComment");
        }

        public static ChatterLikePage GetLikesForFeedElement(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForFeedElement");
        }

        public static ChatterLikePage GetLikesForFeedElement(string communityId, string feedElementId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForFeedElement");
        }

        public static ChatterLikePage GetLikesForFeedItem(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForFeedItem");
        }

        public static ChatterLikePage GetLikesForFeedItem(string communityId, string feedItemId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetLikesForFeedItem");
        }

        public static RelatedFeedPosts GetRelatedPosts(string communityId, string feedElementId,
            RelatedFeedPostType filter, int maxResults)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetRelatedPosts");
        }

        public static ChatterStream GetStream(string communityId, string streamId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetStream");
        }

        public static ChatterStreamPage GetStreams(string communityId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetStreams");
        }

        public static ChatterStreamPage GetStreams(string communityId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetStreams");
        }

        public static SupportedEmojis GetSupportedEmojis()
        {
            throw new global::System.NotImplementedException("ChatterFeeds.GetSupportedEmojis");
        }

        public static FeedEntityIsEditable IsCommentEditableByMe(string communityId, string commentId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.IsCommentEditableByMe");
        }

        public static FeedEntityIsEditable IsFeedElementEditableByMe(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.IsFeedElementEditableByMe");
        }

        public static FeedModifiedInfo IsModified(string communityId, FeedType feedType, string subjectId, string since)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.IsModified");
        }

        public static ChatterLike LikeComment(string communityId, string commentId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.LikeComment");
        }

        public static ChatterLike LikeFeedElement(string communityId, string feedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.LikeFeedElement");
        }

        public static ChatterLike LikeFeedItem(string communityId, string feedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.LikeFeedItem");
        }

        public static Comment PostComment(string communityId, string feedItemId, CommentInput comment,
            BinaryInput feedItemFileUpload)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostComment");
        }

        public static Comment PostComment(string communityId, string feedItemId, string text)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostComment");
        }

        public static Comment PostCommentToFeedElement(string communityId, string feedElementId, CommentInput comment,
            BinaryInput feedElementFileUpload)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostCommentToFeedElement");
        }

        public static Comment PostCommentToFeedElement(string communityId, string feedElementId, string text)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostCommentToFeedElement");
        }

        public static FeedElement PostFeedElement(string communityId, FeedElementInput feedElement)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedElement");
        }

        public static FeedElement PostFeedElement(string communityId, FeedElementInput feedElement,
            BinaryInput feedElementFileUpload)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedElement");
        }

        public static FeedElement PostFeedElement(string communityId, string subjectId, FeedElementType feedElementType,
            string text)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedElement");
        }

        public static List<BatchResult> PostFeedElementBatch(string communityId, List<BatchInput> feedElements)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedElementBatch");
        }

        public static FeedItem PostFeedItem(string communityId, FeedType feedType, string subjectId,
            FeedItemInput feedItem, BinaryInput feedItemFileUpload)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedItem");
        }

        public static FeedItem PostFeedItem(string communityId, FeedType feedType, string subjectId, string text)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.PostFeedItem");
        }

        public static FeedElementPage SearchFeedElements(string communityId, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElements");
        }

        public static FeedElementPage SearchFeedElements(string communityId, string q, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElements");
        }

        public static FeedElementPage SearchFeedElements(string communityId, string q, int recentCommentCount,
            string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElements");
        }

        public static FeedElementPage SearchFeedElements(string communityId, string q, string pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElements");
        }

        public static FeedElementPage SearchFeedElements(string communityId, string q, string pageParam, int pageSize,
            FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElements");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedFilter filter)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFeed");
        }

        public static FeedElementPage SearchFeedElementsInFilterFeed(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFilterFeed");
        }

        public static FeedElementPage SearchFeedElementsInFilterFeed(string communityId, string subjectId,
            string keyPrefix, string pageParam, int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFilterFeed");
        }

        public static FeedElementPage SearchFeedElementsInFilterFeed(string communityId, string subjectId,
            string keyPrefix, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedElementsInFilterFeed");
        }

        public static FeedItemPage SearchFeedItems(string communityId, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItems");
        }

        public static FeedItemPage SearchFeedItems(string communityId, string q, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItems");
        }

        public static FeedItemPage SearchFeedItems(string communityId, string q, int recentCommentCount,
            string pageParam, int pageSize, FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItems");
        }

        public static FeedItemPage SearchFeedItems(string communityId, string q, string pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItems");
        }

        public static FeedItemPage SearchFeedItems(string communityId, string q, string pageParam, int pageSize,
            FeedSortOrder sortParam)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItems");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFeed");
        }

        public static FeedItemPage SearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFilterFeed");
        }

        public static FeedItemPage SearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFilterFeed");
        }

        public static FeedItemPage SearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchFeedItemsInFilterFeed");
        }

        public static ChatterStreamPage SearchStreams(string communityId, string q)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchStreams");
        }

        public static ChatterStreamPage SearchStreams(string communityId, string q, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SearchStreams");
        }

        public static StatusCapability SetFeedCommentStatus(string communityId, string commentId,
            StatusCapabilityInput status)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetFeedCommentStatus");
        }

        public static StatusCapability SetFeedEntityStatus(string communityId, string feedElementId,
            StatusCapabilityInput status)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetFeedEntityStatus");
        }

        public static MuteCapability SetIsMutedByMe(string communityId, string feedElementId, bool isMutedByMe)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetIsMutedByMe");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam, FeedFilter filter,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            bool showInternalOnly, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, bool showInternalOnly, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, bool showInternalOnly, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFeed");
        }

        public static void SetTestGetFeedElementsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFilterFeed");
        }

        public static void SetTestGetFeedElementsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFilterFeed");
        }

        public static void SetTestGetFeedElementsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsFromFilterFeed");
        }

        public static void SetTestGetFeedElementsFromFilterFeedUpdatedSince(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam,
            int pageSize, string updatedSince, FeedElementPage result)
        {
            throw new global::System.NotImplementedException(
                "ChatterFeeds.SetTestGetFeedElementsFromFilterFeedUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            bool showInternalOnly, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            string updatedSince, bool showInternalOnly, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            string updatedSince, bool showInternalOnly, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedElementsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, int elementsPerBundle, FeedDensity density, string pageParam, int pageSize,
            string updatedSince, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedElementsUpdatedSince");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            bool showInternalOnly, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFeed");
        }

        public static void SetTestGetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFilterFeed");
        }

        public static void SetTestGetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFilterFeed");
        }

        public static void SetTestGetFeedItemsFromFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsFromFilterFeed");
        }

        public static void SetTestGetFeedItemsFromFilterFeedUpdatedSince(string communityId, string subjectId,
            string keyPrefix, int recentCommentCount, FeedDensity density, string pageParam, int pageSize,
            string updatedSince, FeedItemPage result)
        {
            throw new global::System.NotImplementedException(
                "ChatterFeeds.SetTestGetFeedItemsFromFilterFeedUpdatedSince");
        }

        public static void SetTestGetFeedItemsUpdatedSince(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsUpdatedSince");
        }

        public static void SetTestGetFeedItemsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            bool showInternalOnly, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsUpdatedSince");
        }

        public static void SetTestGetFeedItemsUpdatedSince(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, string updatedSince,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetFeedItemsUpdatedSince");
        }

        public static void SetTestGetRelatedPosts(string communityId, string feedElementId, RelatedFeedPostType filter,
            int maxResults, RelatedFeedPosts result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestGetRelatedPosts");
        }

        public static void SetTestSearchFeedElements(string communityId, string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElements");
        }

        public static void SetTestSearchFeedElements(string communityId, string q, FeedSortOrder sortParam,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElements");
        }

        public static void SetTestSearchFeedElements(string communityId, string q, int recentCommentCount,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElements");
        }

        public static void SetTestSearchFeedElements(string communityId, string q, string pageParam, int pageSize,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElements");
        }

        public static void SetTestSearchFeedElements(string communityId, string q, string pageParam, int pageSize,
            FeedSortOrder sortParam, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElements");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string q,
            FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedFilter filter, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFeed(string communityId, FeedType feedType, string subjectId,
            string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFeed");
        }

        public static void SetTestSearchFeedElementsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFilterFeed");
        }

        public static void SetTestSearchFeedElementsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFilterFeed");
        }

        public static void SetTestSearchFeedElementsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string q, FeedElementPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedElementsInFilterFeed");
        }

        public static void SetTestSearchFeedItems(string communityId, string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItems");
        }

        public static void SetTestSearchFeedItems(string communityId, string q, FeedSortOrder sortParam,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItems");
        }

        public static void SetTestSearchFeedItems(string communityId, string q, int recentCommentCount,
            string pageParam, int pageSize, FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItems");
        }

        public static void SetTestSearchFeedItems(string communityId, string q, string pageParam, int pageSize,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItems");
        }

        public static void SetTestSearchFeedItems(string communityId, string q, string pageParam, int pageSize,
            FeedSortOrder sortParam, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItems");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, int recentCommentCount,
            FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam, string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string pageParam,
            int pageSize, FeedSortOrder sortParam, string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string q,
            FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, bool showInternalOnly, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFeed(string communityId, FeedType feedType, string subjectId,
            string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFeed");
        }

        public static void SetTestSearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            int recentCommentCount, FeedDensity density, string pageParam, int pageSize, FeedSortOrder sortParam,
            string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFilterFeed");
        }

        public static void SetTestSearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string pageParam, int pageSize, FeedSortOrder sortParam, string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFilterFeed");
        }

        public static void SetTestSearchFeedItemsInFilterFeed(string communityId, string subjectId, string keyPrefix,
            string q, FeedItemPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchFeedItemsInFilterFeed");
        }

        public static void SetTestSearchStreams(string communityId, string q, ChatterStreamPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchStreams");
        }

        public static void SetTestSearchStreams(string communityId, string q, int pageParam, int pageSize,
            ChatterStreamPage result)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.SetTestSearchStreams");
        }

        public static FeedElement ShareFeedElement(string communityId, string subjectId,
            FeedElementType feedElementType, string originalFeedElementId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.ShareFeedElement");
        }

        public static FeedItem ShareFeedItem(string communityId, FeedType feedType, string subjectId,
            string originalFeedItemId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.ShareFeedItem");
        }

        public static FeedItem UpdateBookmark(string communityId, string feedItemId, bool isBookmarkedByCurrentUser)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateBookmark");
        }

        public static Comment UpdateComment(string communityId, string commentId, CommentInput comment)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateComment");
        }

        public static FeedElement UpdateFeedElement(string communityId, string feedElementId,
            FeedElementInput feedElement)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateFeedElement");
        }

        public static BookmarksCapability UpdateFeedElementBookmarks(string communityId, string feedElementId,
            bool isBookmarkedByCurrentUser)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateFeedElementBookmarks");
        }

        public static BookmarksCapability UpdateFeedElementBookmarks(string communityId, string feedElementId,
            BookmarksCapabilityInput bookmarks)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateFeedElementBookmarks");
        }

        public static ChatterLikePage UpdateLikeForComment(string communityId, string commentId,
            bool isLikedByCurrentUser)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateLikeForComment");
        }

        public static ChatterLikePage UpdateLikeForFeedElement(string communityId, string feedElementId,
            bool isLikedByCurrentUser)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateLikeForFeedElement");
        }

        public static ChatterStream UpdateStream(string communityId, string streamId, ChatterStreamInput streamInput)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.UpdateStream");
        }

        public static PollCapability VoteOnFeedElementPoll(string communityId, string feedElementId, string myChoiceId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.VoteOnFeedElementPoll");
        }

        public static FeedPoll VoteOnFeedPoll(string communityId, string feedItemId, string myChoiceId)
        {
            throw new global::System.NotImplementedException("ChatterFeeds.VoteOnFeedPoll");
        }
    }
}