using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace ApexSharp.ApexParser.Toolbox
{
    public interface ICommentParserProvider
    {
        IComment CommentParser { get; }
    }
}
