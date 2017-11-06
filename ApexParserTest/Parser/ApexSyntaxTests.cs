using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class ApexSyntaxTests
    {
        [Test]
        public void DescendantNodesAndSelfForClassEnumReturnsDescendantNodesAndSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassEnum);
            var nodes = syntax.DescendantNodesAndSelf().ToArray();
            Assert.AreEqual(4, nodes.Length);
            Assert.IsInstanceOf<EnumDeclarationSyntax>(nodes[0]);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[1]);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[2]);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[3]);
        }

        [Test]
        public void DescendantNodesForClassEnumReturnsDescendantNodesWithoutSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassEnum);
            var nodes = syntax.DescendantNodes().ToArray();
            Assert.AreEqual(3, nodes.Length);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[0]);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[1]);
            Assert.IsInstanceOf<EnumMemberDeclarationSyntax>(nodes[2]);
        }

        [Test]
        public void DescendantNodesAndSelfForClassInterfaceReturnsDescendantNodesAndSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassInterface);
            var nodes = syntax.DescendantNodesAndSelf().ToArray();
            Assert.AreEqual(10, nodes.Length);
            Assert.IsInstanceOf<ClassDeclarationSyntax>(nodes[0]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[1]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[2]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[3]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[4]);
            Assert.IsInstanceOf<StatementSyntax>(nodes[5]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[6]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[7]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[8]);
            Assert.IsInstanceOf<StatementSyntax>(nodes[9]);
        }

        [Test]
        public void DescendantNodesForClassInterfaceReturnsDescendantNodesWithoutSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassInterface);
            var nodes = syntax.DescendantNodes().ToArray();
            Assert.AreEqual(9, nodes.Length);
            Assert.IsInstanceOf<TypeSyntax>(nodes[0]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[1]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[2]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[3]);
            Assert.IsInstanceOf<StatementSyntax>(nodes[4]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[5]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[6]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[7]);
            Assert.IsInstanceOf<StatementSyntax>(nodes[8]);
        }

        [Test]
        public void DescendantNodesAndSelfForClassInterfaceWithFilterReturnsDescendantNodesAndSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassInterface);
            var nodes = syntax.DescendantNodesAndSelf(x => !(x is BlockSyntax)).ToArray();
            Assert.AreEqual(8, nodes.Length);
            Assert.IsInstanceOf<ClassDeclarationSyntax>(nodes[0]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[1]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[2]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[3]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[4]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[5]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[6]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[7]);
        }

        [Test]
        public void DescendantNodesAndSelfForClassTwoReturnsDescendantNodesAndSelf()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(ClassTwo);
            var nodes = syntax.DescendantNodesAndSelf().ToArray();
            Assert.AreEqual(10, nodes.Length);
            Assert.IsInstanceOf<ClassDeclarationSyntax>(nodes[0]);
            Assert.IsInstanceOf<ConstructorDeclarationSyntax>(nodes[1]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[2]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[3]);
            Assert.IsInstanceOf<StatementSyntax>(nodes[4]);
            Assert.IsInstanceOf<MethodDeclarationSyntax>(nodes[5]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[6]);
            Assert.IsInstanceOf<ParameterSyntax>(nodes[7]);
            Assert.IsInstanceOf<TypeSyntax>(nodes[8]);
            Assert.IsInstanceOf<BlockSyntax>(nodes[9]);
        }

        [Test]
        public void CanLocateSpecificNodesInSoqlDemo2SyntaxTree()
        {
            var syntax = ApexParser.ApexParser.GetApexAst(SoqlDemo2);
            var nodes = syntax.DescendantNodesAndSelf().ToArray();

            var deleteWorked = nodes.OfType<StatementSyntax>().FirstOrDefault(n => n.Body == "System.debug('Delete Worked')");
            Assert.NotNull(deleteWorked);
            Assert.AreEqual(1, deleteWorked.DescendantNodesAndSelf().Count());

            var forEachOverSoql = nodes.OfType<ForEachStatementSyntax>().FirstOrDefault(n => n.Expression.Contains("SELECT"));
            Assert.NotNull(forEachOverSoql);
            Assert.AreEqual(4, forEachOverSoql.DescendantNodesAndSelf().Count());

            var runAsStatement = nodes.OfType<RunAsStatementSyntax>().SingleOrDefault();
            Assert.NotNull(runAsStatement);
            Assert.AreEqual(16, runAsStatement.DescendantNodesAndSelf().Count());
        }
    }
}
