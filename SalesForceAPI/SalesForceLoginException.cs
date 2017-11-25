using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForceAPI
{
    public class SalesForceLoginException : Exception
    {

        public SalesForceLoginException(string message)
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
}
