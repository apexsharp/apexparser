namespace Apex.ConnectApi
{
    public class Recommendations
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("Recommendations.Clone");
        }

        public static RecommendationAudience CreateRecommendationAudience(string communityId,
            RecommendationAudienceInput recommendationAudience)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateRecommendationAudience");
        }

        public static RecommendationAudience CreateRecommendationAudience(string communityId, string name)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateRecommendationAudience");
        }

        public static RecommendationDefinition CreateRecommendationDefinition(string communityId,
            RecommendationDefinitionInput recommendationDefinition)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateRecommendationDefinition");
        }

        public static RecommendationDefinition CreateRecommendationDefinition(string communityId, string name,
            string title, string actionUrl, string actionUrlName, string explanation)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateRecommendationDefinition");
        }

        public static ScheduledRecommendation CreateScheduledRecommendation(string communityId,
            ScheduledRecommendationInput scheduledRecommendation)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateScheduledRecommendation");
        }

        public static ScheduledRecommendation CreateScheduledRecommendation(string communityId,
            string recommendationDefinitionId, int rank, bool enabled, string recommendationAudienceId)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateScheduledRecommendation");
        }

        public static ScheduledRecommendation CreateScheduledRecommendation(string communityId,
            string recommendationDefinitionId, int rank, bool enabled, string recommendationAudienceId,
            RecommendationChannel channel)
        {
            throw new global::System.NotImplementedException("Recommendations.CreateScheduledRecommendation");
        }

        public static void DeleteRecommendationAudience(string communityId, string recommendationAudienceId)
        {
            throw new global::System.NotImplementedException("Recommendations.DeleteRecommendationAudience");
        }

        public static void DeleteRecommendationDefinition(string communityId, string recommendationDefinitionId)
        {
            throw new global::System.NotImplementedException("Recommendations.DeleteRecommendationDefinition");
        }

        public static void DeleteRecommendationDefinitionPhoto(string communityId, string recommendationDefinitionId)
        {
            throw new global::System.NotImplementedException("Recommendations.DeleteRecommendationDefinitionPhoto");
        }

        public static void DeleteScheduledRecommendation(string communityId, string scheduledRecommendationId,
            bool deleteDefinitionIfLast)
        {
            throw new global::System.NotImplementedException("Recommendations.DeleteScheduledRecommendation");
        }

        public static RecommendationAudience GetRecommendationAudience(string communityId,
            string recommendationAudienceId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationAudience");
        }

        public static UserReferencePage GetRecommendationAudienceMembership(string communityId,
            string recommendationAudienceId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationAudienceMembership");
        }

        public static UserReferencePage GetRecommendationAudienceMembership(string communityId,
            string recommendationAudienceId, int pageParam, int pageSize)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationAudienceMembership");
        }

        public static RecommendationAudiencePage GetRecommendationAudiences(string communityId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationAudiences");
        }

        public static RecommendationAudiencePage GetRecommendationAudiences(string communityId, int pageParam,
            int pageSize)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationAudiences");
        }

        public static RecommendationDefinition GetRecommendationDefinition(string communityId,
            string recommendationDefinitionId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationDefinition");
        }

        public static Photo GetRecommendationDefinitionPhoto(string communityId, string recommendationDefinitionId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationDefinitionPhoto");
        }

        public static RecommendationDefinitionPage GetRecommendationDefinitions(string communityId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationDefinitions");
        }

        public static RecommendationCollection GetRecommendationForUser(string communityId, string userId,
            RecommendationActionType action, string objectId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, RecommendationActionType contextAction, string contextObjectId,
            RecommendationChannel channel, int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, RecommendationActionType contextAction, string contextObjectId,
            int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, string objectCategory, RecommendationActionType contextAction,
            string contextObjectId, RecommendationChannel channel, int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, string objectCategory, RecommendationActionType contextAction,
            string contextObjectId, int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType contextAction, string contextObjectId, RecommendationChannel channel,
            int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static RecommendationCollection GetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType contextAction, string contextObjectId, int maxResults)
        {
            throw new global::System.NotImplementedException("Recommendations.GetRecommendationsForUser");
        }

        public static ScheduledRecommendation GetScheduledRecommendation(string communityId,
            string scheduledRecommendationId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetScheduledRecommendation");
        }

        public static ScheduledRecommendationPage GetScheduledRecommendations(string communityId)
        {
            throw new global::System.NotImplementedException("Recommendations.GetScheduledRecommendations");
        }

        public static ScheduledRecommendationPage GetScheduledRecommendations(string communityId,
            RecommendationChannel channel)
        {
            throw new global::System.NotImplementedException("Recommendations.GetScheduledRecommendations");
        }

        public static void RejectRecommendationForUser(string communityId, string userId,
            RecommendationActionType action, RecommendedObjectType objectEnum)
        {
            throw new global::System.NotImplementedException("Recommendations.RejectRecommendationForUser");
        }

        public static void RejectRecommendationForUser(string communityId, string userId,
            RecommendationActionType action, string objectId)
        {
            throw new global::System.NotImplementedException("Recommendations.RejectRecommendationForUser");
        }

        public static void SetTestGetRecommendationForUser(string communityId, string userId,
            RecommendationActionType action, string objectId, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, RecommendationActionType contextAction, string contextObjectId,
            RecommendationChannel channel, int maxResults, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, RecommendationActionType contextAction, string contextObjectId,
            int maxResults, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, string objectCategory, RecommendationActionType contextAction,
            string contextObjectId, RecommendationChannel channel, int maxResults, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType action, string objectCategory, RecommendationActionType contextAction,
            string contextObjectId, int maxResults, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType contextAction, string contextObjectId, RecommendationChannel channel,
            int maxResults, RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static void SetTestGetRecommendationsForUser(string communityId, string userId,
            RecommendationActionType contextAction, string contextObjectId, int maxResults,
            RecommendationCollection result)
        {
            throw new global::System.NotImplementedException("Recommendations.SetTestGetRecommendationsForUser");
        }

        public static RecommendationAudience UpdateRecommendationAudience(string communityId,
            string recommendationAudienceId, RecommendationAudienceInput recommendationAudience)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateRecommendationAudience");
        }

        public static RecommendationDefinition UpdateRecommendationDefinition(string communityId,
            string recommendationDefinitionId, RecommendationDefinitionInput recommendationDefinition)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateRecommendationDefinition");
        }

        public static RecommendationDefinition UpdateRecommendationDefinition(string communityId,
            string recommendationDefinitionId, string name, string title, string actionUrl, string actionUrlName,
            string explanation)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateRecommendationDefinition");
        }

        public static Photo UpdateRecommendationDefinitionPhoto(string communityId, string recommendationDefinitionId,
            BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateRecommendationDefinitionPhoto");
        }

        public static Photo UpdateRecommendationDefinitionPhoto(string communityId, string recommendationDefinitionId,
            string fileId, int versionNumber)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateRecommendationDefinitionPhoto");
        }

        public static Photo UpdateRecommendationDefinitionPhotoWithAttributes(string communityId,
            string recommendationDefinitionId, PhotoInput photo)
        {
            throw new global::System.NotImplementedException(
                "Recommendations.UpdateRecommendationDefinitionPhotoWithAttributes");
        }

        public static Photo UpdateRecommendationDefinitionPhotoWithAttributes(string communityId,
            string recommendationDefinitionId, PhotoInput photo, BinaryInput fileUpload)
        {
            throw new global::System.NotImplementedException(
                "Recommendations.UpdateRecommendationDefinitionPhotoWithAttributes");
        }

        public static ScheduledRecommendation UpdateScheduledRecommendation(string communityId,
            string scheduledRecommendationId, ScheduledRecommendationInput scheduledRecommendation)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateScheduledRecommendation");
        }

        public static ScheduledRecommendation UpdateScheduledRecommendation(string communityId,
            string scheduledRecommendationId, int rank, bool enabled, string recommendationAudienceId)
        {
            throw new global::System.NotImplementedException("Recommendations.UpdateScheduledRecommendation");
        }
    }
}