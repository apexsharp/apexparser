using Apex.System;

namespace Apex.Social
{
    public class DefaultInboundSocialPostHandler
    {
        public object Clone()
        {
            throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.Clone");
        }

        //public SObject CreatePersonaParent(SocialPersona persona){throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.CreatePersonaParent");}
        public string GetDefaultAccountId()
        {
            throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.GetDefaultAccountId");
        }

        public int GetMaxNumberOfDaysClosedToReopenCase()
        {
            throw new global::System.NotImplementedException(
                "DefaultInboundSocialPostHandler.GetMaxNumberOfDaysClosedToReopenCase");
        }

        //public string GetPersonaFirstName(SocialPersona persona){throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.GetPersonaFirstName");}
        //public string GetPersonaLastName(SocialPersona persona){throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.GetPersonaLastName");}
        public Set<String> GetPostTagsThatCreateCase()
        {
            throw new global::System.NotImplementedException(
                "DefaultInboundSocialPostHandler.GetPostTagsThatCreateCase");
        }

        //public Social.InboundSocialPostResult HandleInboundSocialPost(SocialPost post,SocialPersona persona,Map<String,ANY> rawData){throw new global::System.NotImplementedException("DefaultInboundSocialPostHandler.HandleInboundSocialPost");}
    }
}