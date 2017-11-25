using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForceAPI
{
    public class SalesForceInvalidLoginException : Exception
    {

        public SalesForceInvalidLoginException(string message)
        : base(message)
        {
        }

    }

    public class SalesForceNoFileFoundException : Exception
    {

        public SalesForceNoFileFoundException(string message)
        : base(message)
        {
        }

    }

    public class SalesSessionExpiredException : Exception
    {

        public SalesSessionExpiredException(string message)
        : base(message)
        {
        }

    }

    public class ApexSharpHttpException : Exception
    {

        public ApexSharpHttpException(string message)
        : base(message)
        {
        }

    }
}
