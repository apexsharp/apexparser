using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.ParserAst;

namespace ApexParser.Visitors
{
    public abstract class BaseVisitor
    {
        public abstract void VisitClassDeclaration(ClassDeclaration cd);

        public abstract void VisitMethodDeclaration(MethodDeclaration md);

        public abstract void VisitMethodParameters(MethodParameters mp);

        public abstract void VisitParameterDeclaration(ParameterDeclaration pd);
    }
}
