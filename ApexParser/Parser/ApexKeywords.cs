using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.Parser
{
    public static class ApexKeywords
    {
        public static HashSet<string> All { get; } =
            new HashSet<string>(AllStringConstants);

        private static IEnumerable<string> AllStringConstants =>
            typeof(ApexKeywords).GetFields().Select(f => f.GetValue(null)).OfType<string>();

        public const string Abstract = "abstract";
        public const string Boolean = "boolean";
        public const string Break = "break";
        public const string Byte = "byte";
        public const string Catch = "catch";
        public const string Char = "char";
        public const string Class = "class";
        public const string Const = "const";
        public const string Continue = "continue";
        public const string Database = "database";
        public const string Default = "default";
        public const string Do = "do";
        public const string Double = "double";
        public const string Else = "else";
        public const string Enum = "enum";
        public const string Extends = "extends";
        public const string Final = "final";
        public const string Finally = "finally";
        public const string Float = "float";
        public const string For = "for";
        public const string If = "if";
        public const string Get = "get";
        public const string Global = "global"; // apex
        public const string Goto = "goto";
        public const string Implements = "implements";
        public const string Import = "import";
        public const string InstanceOf = "instanceof";
        public const string Int = "int";
        public const string Interface = "interface";
        public const string Long = "long";
        public const string Native = "native";
        public const string New = "new";
        public const string Override = "override";
        public const string Package = "package";
        public const string Private = "private";
        public const string Protected = "protected";
        public const string Public = "public";
        public const string Return = "return";
        public const string Set = "set";
        public const string Sharing = "sharing";
        public const string Short = "short";
        public const string Static = "static";
        public const string Super = "super";
        public const string TestMethod = "testMethod";
        public const string This = "this";
        public const string Throw = "throw";
        public const string Throws = "throws";
        public const string Transient = "transient";
        public const string Try = "try";
        public const string Synchronized = "synchronized";
        public const string Virtual = "virtual";
        public const string Void = "void";
        public const string Volatile = "volatile";
        public const string WebService = "webservice"; // apex
        public const string While = "while";
        public const string With = "with"; // apex
        public const string Without = "without"; // apex

        /*
        // Embedded database language keywords
        SELECT        : S E L E C T;
        DB_INSERT     : I N S E R T;
        DB_UPSERT     : U P S E R T;
        DB_UPDATE     : U P D A T E;
        DB_DELETE     : D E L E T E;
        DB_UNDELETE   : U N D E L E T E;
        */
    }
}
