using Apex.System;

namespace Apex.KbManagement
{
    public class PublishingService
    {
        public PublishingService()
        {
            throw new global::System.NotImplementedException("PublishingService");
        }

        public static void ArchiveOnlineArticle(string articleId, DateTime scheduledDate)
        {
            throw new global::System.NotImplementedException("PublishingService.ArchiveOnlineArticle");
        }

        public static void AssignDraftArticleTask(string articleId, string assigneeId, string instructions,
            DateTime dueDate, bool sendEmailNotification)
        {
            throw new global::System.NotImplementedException("PublishingService.AssignDraftArticleTask");
        }

        public static void AssignDraftTranslationTask(string translationVersionId, string assigneeId,
            string instructions, DateTime dueDate, bool sendEmailNotification)
        {
            throw new global::System.NotImplementedException("PublishingService.AssignDraftTranslationTask");
        }

        public static void CancelScheduledArchivingOfArticle(string articleId)
        {
            throw new global::System.NotImplementedException("PublishingService.CancelScheduledArchivingOfArticle");
        }

        public static void CancelScheduledPublicationOfArticle(string articleId)
        {
            throw new global::System.NotImplementedException("PublishingService.CancelScheduledPublicationOfArticle");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("PublishingService.Clone");
        }

        public static void CompleteTranslation(string articleVersionId)
        {
            throw new global::System.NotImplementedException("PublishingService.CompleteTranslation");
        }

        public static void DeleteArchivedArticle(string articleId)
        {
            throw new global::System.NotImplementedException("PublishingService.DeleteArchivedArticle");
        }

        public static void DeleteArchivedArticleVersion(string articleId, int versionNumber)
        {
            throw new global::System.NotImplementedException("PublishingService.DeleteArchivedArticleVersion");
        }

        public static void DeleteDraftArticle(string articleId)
        {
            throw new global::System.NotImplementedException("PublishingService.DeleteDraftArticle");
        }

        public static void DeleteDraftTranslation(string articleVersionId)
        {
            throw new global::System.NotImplementedException("PublishingService.DeleteDraftTranslation");
        }

        public static string EditArchivedArticle(string articleId)
        {
            throw new global::System.NotImplementedException("PublishingService.EditArchivedArticle");
        }

        public static string EditOnlineArticle(string articleId, bool unpublish)
        {
            throw new global::System.NotImplementedException("PublishingService.EditOnlineArticle");
        }

        public static string EditPublishedTranslation(string articleId, string language, bool unpublish)
        {
            throw new global::System.NotImplementedException("PublishingService.EditPublishedTranslation");
        }

        public static void PublishArticle(string articleId, bool flagAsNew)
        {
            throw new global::System.NotImplementedException("PublishingService.PublishArticle");
        }

        public static string RestoreOldVersion(string articleId, int versionNumber)
        {
            throw new global::System.NotImplementedException("PublishingService.RestoreOldVersion");
        }

        public static void ScheduleForPublication(string articleId, DateTime scheduledDate)
        {
            throw new global::System.NotImplementedException("PublishingService.ScheduleForPublication");
        }

        public static void SetTranslationToIncomplete(string articleVersionId)
        {
            throw new global::System.NotImplementedException("PublishingService.SetTranslationToIncomplete");
        }

        public static string SubmitForTranslation(string articleId, string language, string assigneeId,
            DateTime dueDate)
        {
            throw new global::System.NotImplementedException("PublishingService.SubmitForTranslation");
        }
    }
}