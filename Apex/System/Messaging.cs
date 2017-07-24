using Apex.Messaging;

namespace Apex.System
{
    public class Messaging
    {
        public static List<RenderEmailTemplateBodyResult> RenderEmailTemplate(string whoId, string whatId,
            List<string> bodies)
        {
            throw new global::System.NotImplementedException("Messaging.RenderEmailTemplate");
        }

        public static SingleEmailMessage RenderStoredEmailTemplate(string templateId, string whoId, string whatId)
        {
            throw new global::System.NotImplementedException("Messaging.RenderStoredEmailTemplate");
        }

        public static void ReserveMassEmailCapacity(int count)
        {
            throw new global::System.NotImplementedException("Messaging.ReserveMassEmailCapacity");
        }

        public static void ReserveSingleEmailCapacity(int count)
        {
            throw new global::System.NotImplementedException("Messaging.ReserveSingleEmailCapacity");
        }

        public static List<SendEmailResult> SendEmail(List<Email> emailMessages)
        {
            throw new global::System.NotImplementedException("Messaging.SendEmail");
        }

        public static List<SendEmailResult> SendEmail(List<Email> emailMessages, bool allOrNothing)
        {
            throw new global::System.NotImplementedException("Messaging.SendEmail");
        }

        public static List<SendEmailResult> SendEmailMessage(List<Id> emailMessagesIds)
        {
            throw new global::System.NotImplementedException("Messaging.SendEmailMessage");
        }

        public static List<SendEmailResult> SendEmailMessage(List<Id> emailMessagesIds, bool allOrNothing)
        {
            throw new global::System.NotImplementedException("Messaging.SendEmailMessage");
        }
    }
}