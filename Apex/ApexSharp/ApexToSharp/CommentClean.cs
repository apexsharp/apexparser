using System.Collections.Generic;

namespace Apex.ApexSharp.ApexToSharp
{
    public class CommentClean
    {
        // Remove Comments

        public static List<ApexTocken> RemoveApexTokens(List<ApexTocken> apexTokenList)
        {
            List<ApexTocken> newTokenList = new List<ApexTocken>();

            bool insideComment = false;
            foreach (var apexToken in apexTokenList)
            {
                if (apexToken.TockenType == TockenType.CommentStart)
                {
                    insideComment = true;
                }
                else if (apexToken.TockenType == TockenType.CommentEnd)
                {
                    insideComment = false;
                }
                else if (insideComment == false)
                {
                    newTokenList.Add(apexToken);
                }
            }
            return newTokenList;
        }

        //// Merges the Comments. 
        //var apexTokens = new List<ApexTocken>(1000);
        //var commentCode = String.Empty;
        //bool inCommentCode = false;
        //        foreach (var apexTokenComments in apexTokensWithComments.ToList())
        //        {
        //            if (apexTokenComments.TockenType == TockenType.CommentStart)
        //            {
        //                apexTokens.Add(new ApexTocken(TockenType.CommentLine, "/*"));
        //                inCommentCode = true;
        //            }
        //            else if (inCommentCode)
        //            {
        //                if (apexTokenComments.TockenType == TockenType.Return)
        //                {
        //                    commentCode = commentCode.Trim();
        //                    if (commentCode.Length != 0)
        //                    {
        //                        apexTokens.Add(new ApexTocken(TockenType.CommentLine, commentCode));
        //                        commentCode = String.Empty;
        //                    }
        //                }
        //                else if (apexTokenComments.TockenType == TockenType.CommentEnd)
        //                {
        //                    apexTokens.Add(new ApexTocken(TockenType.CommentLine, "*/"));
        //                    inCommentCode = false;
        //                }
        //                else
        //                {
        //                    commentCode = commentCode + apexTokenComments.Tocken;
        //                }
        //            }
        //            else
        //            {
        //                apexTokens.Add(apexTokenComments);
        //            }
        //        }

        //// Merge all comment Tokens into one Token
        //List<ApexTocken> newApexTockens = new List<ApexTocken>();
        //StringBuilder commentStringBuilder = new StringBuilder();
        //bool inCommentArea = false;
        //        foreach (var apexTocken in apexClass.ApexTokens)
        //        {
        //            if (apexTocken.TockenType == TockenType.CommentStart)
        //            {
        //                commentStringBuilder = new StringBuilder();
        //commentStringBuilder.Append(apexTocken.Tocken);
        //                inCommentArea = true;
        //            }
        //            else if (apexTocken.TockenType == TockenType.CommentEnd)
        //            {
        //                commentStringBuilder.Append(apexTocken.Tocken);
        //                newApexTockens.Add(new ApexTocken(TockenType.CommentLine, sb.ToString()));
        //                inCommentArea = false;
        //            }
        //            else if (inCommentArea)
        //            {
        //                commentStringBuilder.Append(apexTocken.Tocken);
        //            }
        //            else
        //            {
        //                newApexTockens.Add(apexTocken);
        //            }
        //        }
    }
}