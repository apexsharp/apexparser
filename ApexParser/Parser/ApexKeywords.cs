using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.Parser
{
    public static class ApexKeywords
    {
        public static HashSet<string> ReservedWords { get; } =
            new HashSet<string>(AllStringConstants, StringComparer.InvariantCultureIgnoreCase);

        private static IEnumerable<string> AllStringConstants =>
            typeof(ApexKeywords).GetFields().Select(f => f.GetValue(null)).OfType<string>();

        // Keyword DSL convention:
        // 1. Reserved keywords are declared as constants
        // 2. Non-reserved keywords are declared as static properties

        // Reference:
        // https://developer.salesforce.com/docs/atlas.en-us.apexcode.meta/apexcode/apex_reserved_words.htm

        // 1. Reserved words
        public const string Abstract = "abstract";
        public const string Activate = "activate"; // reserved for future use
        public const string And = "and";
        public const string Any = "any"; // reserved for future use
        public const string Array = "array";
        public const string As = "as";
        public const string Asc = "asc";
        public const string Autonomous = "autonomous"; // reserved for future use
        public const string Begin = "begin"; // reserved for future use
        public const string BigDecimal = "bigdecimal"; // reserved for future use
        public const string Blob = "blob";
        public const string Break = "break";
        public const string Bulk = "bulk";
        public const string By = "by";
        public const string Byte = "byte"; // reserved for future use
        public const string Case = "case"; // reserved for future use
        public const string Cast = "cast"; // reserved for future use
        public const string Catch = "catch";
        public const string Char = "char"; // reserved for future use
        public const string Class = "class";
        public const string Collect = "collect"; // reserved for future use
        public const string Commit = "commit";
        public const string Const = "const"; // reserved for future use
        public const string Continue = "continue";
        public const string ConvertCurrency = "convertcurrency";
        public const string Decimal = "decimal";
        public const string Default = "default"; // reserved for future use
        public const string Delete = "delete";
        public const string Desc = "desc";
        public const string Do = "do";
        public const string Else = "else";
        public const string End = "end"; // reserved for future use
        public const string Enum = "enum";
        public const string Exception = "exception";
        public const string Exit = "exit"; // reserved for future use
        public const string Export = "export"; // reserved for future use
        public const string Extends = "extends";
        public const string False = "false";
        public const string Final = "final";
        public const string Finally = "finally";
        public const string Float = "float"; // reserved for future use
        public const string For = "for";
        public const string From = "from";
        public const string Future = "future";
        public const string Global = "global";
        public const string Goto = "goto"; // reserved for future use
        public const string Group = "group"; // reserved for future use
        public const string Having = "having"; // reserved for future use
        public const string Hint = "hint"; // reserved for future use
        public const string If = "if";
        public const string Implements = "implements";
        public const string Import = "import"; // reserved for future use
        public const string Inner = "inner"; // reserved for future use
        public const string Insert = "insert";
        public const string InstanceOf = "instanceof";
        public const string Interface = "interface";
        public const string Into = "into"; // reserved for future use
        public const string Int = "int";
        public const string Join = "join"; // reserved for future use
        public const string Last90Days = "last_90_days";
        public const string LastMonth = "last_month";
        public const string LastNDays = "last_n_days";
        public const string LastWeek = "last_week";
        public const string Like = "like";
        public const string Limit = "limit";
        public const string List = "list";
        public const string Long = "long";
        public const string Loop = "loop"; // reserved for future use
        public const string Map = "map";
        public const string Merge = "merge";
        public const string New = "new";
        public const string Next90Days = "next_90_days";
        public const string NextMonth = "next_month";
        public const string NextNDays = "next_n_days";
        public const string NextWeek = "next_week";
        public const string Not = "not";
        public const string Null = "null";
        public const string Nulls = "nulls";
        public const string Number = "number"; // reserved for future use
        public const string Object = "object"; // reserved for future use
        public const string Of = "of"; // reserved for future use
        public const string On = "on";
        public const string Or = "or";
        public const string Outer = "outer"; // reserved for future use
        public const string Override = "override";
        public const string Package = "package";
        public const string Parallel = "parallel"; // reserved for future use
        public const string Pragma = "pragma"; // reserved for future use
        public const string Private = "private";
        public const string Protected = "protected";
        public const string Public = "public";
        public const string Retrieve = "retrieve"; // reserved for future use
        public const string Return = "return";
        public const string Returning = "returning"; // reserved for future use
        public const string Rollback = "rollback";
        public const string Savepoint = "savepoint";
        public const string Search = "search"; // reserved for future use
        public const string Select = "select";
        public const string Set = "set"; // "set" is reserved but "get" isn't
        public const string Short = "short"; // reserved for future use
        public const string Sort = "sort";
        public const string Stat = "stat"; // reserved for future use
        public const string Static = "static";
        public const string Super = "super";
        public const string Switch = "switch"; // reserved for future use
        public const string Synchronized = "synchronized"; // reserved for future use
        public const string System = "system";
        public const string TestMethod = "testmethod";
        public const string Then = "then"; // reserved for future use
        public const string This = "this";
        public const string ThisMonth = "this_month"; // reserved for future use
        public const string ThisWeek = "this_week";
        public const string Throw = "throw";
        public const string Today = "today";
        public const string Tolabel = "tolabel";
        public const string Tomorrow = "tomorrow";
        public const string Transaction = "transaction"; // reserved for future use
        public const string Trigger = "trigger";
        public const string True = "true";
        public const string Try = "try";
        public const string Type = "type"; // reserved for future use
        public const string Undelete = "undelete";
        public const string Update = "update";
        public const string Upsert = "upsert";
        public const string Using = "using";
        public const string Virtual = "virtual";
        public const string Webservice = "webservice";
        public const string When = "when"; // reserved for future use
        public const string Where = "where";
        public const string While = "while";
        public const string Yesterday = "yesterday";

        // 2. Not reserved words
        public static string After => "after";
        public static string Before => "before";
        public static string Count => "count";
        public static string Excludes => "excludes";
        public static string First => "first";
        public static string Includes => "includes";
        public static string Last => "last";
        public static string Order => "order";
        public static string Sharing => "sharing";
        public static string With => "with";

        // 3. Not listed in the official documentation but apparently used
        public static string Boolean => "boolean";
        ////public static string Database => "database"; // the status is unclear
        public static string Double => "double";
        public static string Get => "get"; // "set" is reserved but "get" isn't
        ////public const string Native = "native"; // the status is unclear
        ////public const string Throws = "throws"; // the status is unclear
        public static string RunAs => "runas"; // System.runAs has special syntax
        public static string Transient => "transient"; // variable modifier
        public static string Void => "void";
        ////public const string Volatile = "volatile"; // the status is unclear
        public static string WebService => "webservice"; // method modifier
        public static string Without => "without"; // part of "without sharing"

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
