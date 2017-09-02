using Apex.QuickAction;
using SalesForceAPI.Apex;

namespace Apex.System
{
    public class QuickAction
    {
        public static List<DescribeAvailableQuickActionResult> DescribeAvailableQuickActions(string parentType)
        {
            throw new global::System.NotImplementedException("QuickAction.DescribeAvailableQuickActions");
        }

        public static List<DescribeQuickActionResult> DescribeQuickActions(List<string> actions)
        {
            throw new global::System.NotImplementedException("QuickAction.DescribeQuickActions");
        }

        public static QuickActionResult PerformQuickAction(QuickActionRequest performQuickAction)
        {
            throw new global::System.NotImplementedException("QuickAction.PerformQuickAction");
        }

        public static QuickActionResult PerformQuickAction(QuickActionRequest performQuickAction, bool allOrNothing)
        {
            throw new global::System.NotImplementedException("QuickAction.PerformQuickAction");
        }

        public static List<QuickActionResult> PerformQuickActions(List<QuickActionRequest> performQuickActions)
        {
            throw new global::System.NotImplementedException("QuickAction.PerformQuickActions");
        }

        public static List<QuickActionResult> PerformQuickActions(List<QuickActionRequest> performQuickActions,
            bool allOrNothing)
        {
            throw new global::System.NotImplementedException("QuickAction.PerformQuickActions");
        }

        public static QuickActionTemplateResult RetrieveQuickActionTemplate(string quickActionName, Id contextId)
        {
            throw new global::System.NotImplementedException("QuickAction.RetrieveQuickActionTemplate");
        }

        public static List<QuickActionTemplateResult> RetrieveQuickActionTemplates(List<string> quickActionNames,
            Id contextId)
        {
            throw new global::System.NotImplementedException("QuickAction.RetrieveQuickActionTemplates");
        }
    }
}