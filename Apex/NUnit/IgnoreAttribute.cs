using Ignore = NUnit.Framework.IgnoreAttribute;

namespace Apex.NUnit
{
    public class IgnoreAttribute : Ignore
    {
        public IgnoreAttribute(string msg = "TODO") : base(msg)
        {
        }
    }
}
