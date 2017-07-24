using Apex.ApexSharp.ApexToSharp.Lexer;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexTokenList
    {
        public static TokenDefinition[] GetTocken()
        {
            var regExList = new[]
            {
                // Rest Signature. This should be first as it may have \* which is also comment line starter.
                new TokenDefinition(@"(?i)@\s*RestResource\s*\(\s*urlMapping\s*=\s*'(.*?)\)", TockenType.Attrubute),

                new TokenDefinition(@"([\''])(?:\\\1|.)*?\1", TockenType.QuotedString),

                // Comments
                new TokenDefinition(@"\/\*", TockenType.CommentStart),
                new TokenDefinition(@"\*\/", TockenType.CommentEnd),
                new TokenDefinition(@"//::.*", TockenType.ApexError),
                new TokenDefinition(@"//.[^\n\r]*", TockenType.CommentLine),

                // Class
                new TokenDefinition(@"(?i:\bstatic\b)", TockenType.AccessModifier),
                new TokenDefinition(@"(?i:\b(private|protected|public|global)\b)", TockenType.AccessModifier),
                new TokenDefinition(@"(?i:abstract)", TockenType.AccessModifier),
                new TokenDefinition(@"(?i:\bfinal\b)", TockenType.AccessModifier),


                new TokenDefinition(@"\bget\b", TockenType.KwGetSet),
                new TokenDefinition(@"\bset\b", TockenType.KwGetSet),
                new TokenDefinition(@"\binterface\b", TockenType.KwInterface),
                new TokenDefinition(@"\bimplements\b", TockenType.KwImplements),
                new TokenDefinition(@"\bextends\b", TockenType.KwExtends),
                new TokenDefinition(@"\boverride\b", TockenType.KwOverride),
                new TokenDefinition(@"\bwith sharing\b", TockenType.KwWithSharing),
                new TokenDefinition(@"\bwithout sharing\b", TockenType.KwWithoutSharing),
                new TokenDefinition(@"\bclass\b", TockenType.KwClass),
                new TokenDefinition(@"\breturn\b", TockenType.KwReturn),
                new TokenDefinition(@"\bwebservice\b", TockenType.KwWebService),

                // if, for, while, else 
                new TokenDefinition(@"\bfor\b", TockenType.KwFor),
                new TokenDefinition(@"\bif\b", TockenType.KwIf),
                new TokenDefinition(@"\belse\b", TockenType.KwElse),
                new TokenDefinition(@"\bwhile\b", TockenType.KwWhile),

                // exception 
                new TokenDefinition(@"\bcatch\b", TockenType.KwCatch),
                new TokenDefinition(@"\btry\b", TockenType.KwTry),
                new TokenDefinition(@"\bthrow\b", TockenType.KwThrow),
                new TokenDefinition(@"\bfinally\b", TockenType.KwFinally),

                new TokenDefinition(@"\benum\b", TockenType.KwEnum),

                new TokenDefinition(@"\bstatic\b", TockenType.KwStatic),
                new TokenDefinition(@"\bvoid\b", TockenType.KwVoid),
                new TokenDefinition(@"(?i:\btestmethod\b)", TockenType.KwTestMethod),


                new TokenDefinition(@"\b[A-Za-z]*\s*<(.*?)>", TockenType.ClassNameGeneric),
                new TokenDefinition(@"[A-Za-z]*\s*\[\s*\]", TockenType.ClassNameArray),
                new TokenDefinition(@"[A-Za-z]*\s*\[[0-9]\]", TockenType.ClassNameArraySize),

                // SOQL
                new TokenDefinition(@"\[(?i:select).*\]", TockenType.Soql),
                new TokenDefinition(@"(?i:\b(update|delete|undelete|insert)\b)", TockenType.DML),

                // JSON
                new TokenDefinition(@"(?i:JSON.deserialize)", TockenType.JsonDeserialize),
                new TokenDefinition(@"(?i:JSON.serialize)", TockenType.JsonSerialize),
                new TokenDefinition(@"(?i)\.class", TockenType.KwClassType),

                // Testing Attrubute
                new TokenDefinition(@"(?i)\@\s*isTest\s*\(\s*SeeAllData\s*\=\s*(true|false)\s*\)",
                    TockenType.IsTestAttrubute),
                new TokenDefinition(@"(?i)@\s*TestVisible", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*TestSetup", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*IsTest", TockenType.IsTestAttrubute),

                // Rest Attrubute   
                new TokenDefinition(@"(?i)@\s*HttpPost", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpGet", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpPatch", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpPost", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpDelete", TockenType.Attrubute),

                new TokenDefinition(@"(?i)@\s*Future\(callout\=true\)", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*Future", TockenType.Attrubute),

                new TokenDefinition(@"(?i)@\s*RemoteAction", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*Deprecated", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*InvocableMethod", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*InvocableVariable", TockenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*ReadOnly", TockenType.Attrubute),


                new TokenDefinition(@";", TockenType.StatmentTerminator),


                //    new TokenDefinition(@"<(.*?)>", TockenType.ClassNameGeneric),

                //  new TokenDefinition(@"\bList<\b(.*?)>", TockenType.ClassNameGeneric),
                //  new TokenDefinition(@"\Set<\b(.*?)>", TockenType.ClassNameGeneric),
                //  new TokenDefinition(@"\bMap<\b(.*?)>", TockenType.ClassNameGeneric),

                new TokenDefinition(@"\+\+", TockenType.Equal),
                new TokenDefinition(@"\=\=", TockenType.Equal),
                new TokenDefinition(@"\&\&", TockenType.Equal),
                new TokenDefinition(@"\!=", TockenType.Equal),
                new TokenDefinition(@"\=", TockenType.Equal),
                new TokenDefinition(@"\,", TockenType.Comma),
                new TokenDefinition(@":", TockenType.Colon),


                new TokenDefinition(@"[-+]?[0-9]*\.?[0-9]+", TockenType.Decimel),
                new TokenDefinition(@"(?<![-.])\b[0-9]+\b(?!\.[0-9])", TockenType.Integer),


                new TokenDefinition(@"[_A-Za-z0-9!]+", TockenType.Word),


                new TokenDefinition(@"\.", TockenType.Dot),


                new TokenDefinition(@"\r\n", TockenType.Return),
                new TokenDefinition(@"\n", TockenType.Return),


                new TokenDefinition(@"\{", TockenType.OpenCurlyBrackets),
                new TokenDefinition(@"\}", TockenType.CloseCurlyBrackets),

                //   new TokenDefinition(@"\(\)", TockenType.EmptyBrackets),
                new TokenDefinition(@"\(", TockenType.OpenBrackets),
                new TokenDefinition(@"\)", TockenType.CloseBrackets),


                new TokenDefinition(@"\s", TockenType.Space),


                new TokenDefinition(@"\#|\^|\?|\`|\'|\-|\""|\(|\)|\[|\]|\=|\/|\+|\~|\<|\&|\%|\$|\*|\\|\|",
                    TockenType.Anything)
            };

            return regExList;
        }
    }

    public enum TockenType
    {
        Start,
        IsTestAttrubute,
        Attrubute,

        QuotedString,
        Return,


        CommentStart,
        CommentEnd,
        CommentLine,
        ApexError,

        OpenCurlyBrackets,
        CloseCurlyBrackets,
        OpenBrackets,
        CloseBrackets,


        Word,
        Space,
        Anything,
        StatmentTerminator,
        Top,
        End,
        TestAttrubute,
        RestResource,
        RestType,

        // Class Names
        ClassNameGeneric,
        ClassNameArray,
        ClassNameArraySize,
        ClassName,


        MethodName,
        Integer,
        Decimel,

        List,
        Equal,
        Comma,

        AccessModifier,
        Soql,
        JsonDeserialize,
        JsonSerialize,

        // Reserved Words
        KwVoid,
        KwClass,
        KwClassType,
        KwStatic,
        KwOverride,
        KwWithSharing,
        KwWithoutSharing,
        KwGetSet,
        KwCatch,
        KwIf,
        KwElse,
        KwTry,
        KwFor,
        KwReturn,
        KwFinally,
        KwTestMethod,
        KwWebService,
        KwWhile,
        KwThrow,


        KwExtends,
        KwImplements,
        KwInterface,
        KwEnum,


        DML,

        Dot,
        Colon
    }

//abstract
//activate*
//and
//any*
//array
//as
//asc
//autonomous*
//begin*
//bigdecimal*
//blob
//break
//bulk
//by
//byte*
//case*
//cast*
//catch
//char*
//class
//collect*
//commit
//const*
//continue
//convertcurrency
//decimal
//default*
//delete
//desc
//do
//else
//end*
//enum
//exception
//exit*
//export*
//extends
//false
//final
//finally
//float*
//for
//from
//future
//global
//goto*
//group*
//having*
//hint*
//if
//implements
//import*
//inner*
//insert
//instanceof
//interface
//into*
//int
//join*
//last_90_days
//last_month
//last_n_days
//last_week
//like
//limit
//list
//long
//loop*
//map
//merge
//new
//next_90_days
//next_month
//next_n_days
//next_week
//not
//null
//nulls
//number*
//object*
//of*
//on
//or
//outer*
//override
//package
//parallel*
//pragma*
//private
//protected
//public
//retrieve*
//return
//returning*
//rollback
//savepoint
//search*
//select
//set
//short*
//sort
//stat*
//static
//super
//switch*
//synchronized*
//system
//testmethod
//then*
//this
//this_month*
//this_week
//throw
//today
//tolabel
//tomorrow
//transaction*
//trigger
//true
//try
//type*
//undelete
//update
//upsert
//using
//virtual
//webservice
//when*
//where
//while
//yesterday
}