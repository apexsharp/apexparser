namespace Apex
{
    using System;

    public class MokManager
    {
        public static bool UseMok = true;
    }

    public class UserInfo
    {
        public static string GetFirstName()
        {
            if (MokManager.UseMok)
            {
                return Mok.UserInfo.GetFirstName();
            }

            return ApexSharpReal.UserInfo.GetFirstName();
        }
    }
}

namespace ApexSharpReal
{
    public class UserInfo
    {
        public static string GetFirstName()
        {
            throw new global::System.NotImplementedException("UserInfo.GetFirstName");
        }
    }
}

namespace Mok
{
    public class UserInfo
    {
        public static string GetFirstName()
        {
            return "Jay";
        }
    }

}
