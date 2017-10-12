namespace ApexParser.Lexer
{
    public static class ApexTokenRegEx
    {
        public static TokenDefinition[] GetTokenDefinitions()
        {
            var regExList = new[]
            {
                // Rest Signature. This should be first as it may have \* which is also comment line starter.
                new TokenDefinition(@"(?i)@\s*RestResource\s*\(\s*urlMapping\s*=\s*'(.*?)\)", TokenType.Attrubute),

                new TokenDefinition(@"([\''])(?:\\\1|.)*?\1", TokenType.QuotedString),

                // Comments
                new TokenDefinition(@"\/\*", TokenType.CommentStart),
                new TokenDefinition(@"\*\/", TokenType.CommentEnd),
                new TokenDefinition(@"//::.*", TokenType.ApexError),
                new TokenDefinition(@"//.[^\n\r]*", TokenType.CommentLine),

                // Class
                new TokenDefinition(@"(?i:\bstatic\b)", TokenType.KwStatic),
                new TokenDefinition(@"(?i:\b(private|protected|public|global)\b)", TokenType.AccessModifier),
                new TokenDefinition(@"(?i:abstract)", TokenType.AccessModifier),
                new TokenDefinition(@"(?i:\bfinal\b)", TokenType.AccessModifier),

                new TokenDefinition(@"\bget\b", TokenType.KwGetSet),
                new TokenDefinition(@"\bset\b", TokenType.KwGetSet),
                new TokenDefinition(@"\binterface\b", TokenType.KwInterface),
                new TokenDefinition(@"\bimplements\b", TokenType.KwImplements),
                new TokenDefinition(@"\bextends\b", TokenType.KwExtends),
                new TokenDefinition(@"\boverride\b", TokenType.KwOverride),
                new TokenDefinition(@"\bwith sharing\b", TokenType.KwWithSharing),
                new TokenDefinition(@"\bwithout sharing\b", TokenType.KwWithoutSharing),
                new TokenDefinition(@"\bclass\b", TokenType.KwClass),
                new TokenDefinition(@"\breturn\b", TokenType.KwReturn),
                new TokenDefinition(@"\bwebservice\b", TokenType.KwWebService),

                // if, for, while, else
                new TokenDefinition(@"\bfor\b", TokenType.KwFor),
                new TokenDefinition(@"\bif\b", TokenType.KwIf),
                new TokenDefinition(@"\belse\b", TokenType.KwElse),
                new TokenDefinition(@"\bwhile\b", TokenType.KwWhile),

                // exception
                new TokenDefinition(@"\bcatch\b", TokenType.KwCatch),
                new TokenDefinition(@"\btry\b", TokenType.KwTry),
                new TokenDefinition(@"\bthrow\b", TokenType.KwThrow),
                new TokenDefinition(@"\bfinally\b", TokenType.KwFinally),

                new TokenDefinition(@"\benum\b", TokenType.KwEnum),

                new TokenDefinition(@"\bstatic\b", TokenType.KwStatic),
                new TokenDefinition(@"\bvoid\b", TokenType.KwVoid),

                new TokenDefinition(@"(?i:\btestmethod\b)", TokenType.KwTestMethod),

                new TokenDefinition(@"\b[A-Za-z]*\s*<(.*?)>", TokenType.ClassNameGeneric),
                new TokenDefinition(@"[A-Za-z]*\s*\[\s*\]", TokenType.ClassNameArray),
                new TokenDefinition(@"[A-Za-z]*\s*\[[0-9]\]", TokenType.ClassNameArraySize),

                // SOQL
                new TokenDefinition(@"\[(?i:select).*\]", TokenType.Soql),
                new TokenDefinition(@"(?i:\b(update|delete|undelete|insert)\b)", TokenType.DML),

                // JSON
                new TokenDefinition(@"(?i:JSON.deserialize)", TokenType.JsonDeserialize),
                new TokenDefinition(@"(?i:JSON.serialize)", TokenType.JsonSerialize),
                new TokenDefinition(@"(?i)\.class", TokenType.KwClassType),

                // APEX Attributes
                new TokenDefinition(@"(?i)\@\s*isTest\s*\(\s*SeeAllData\s*\=\s*(true|false)\s*\)", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*TestVisible", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*TestSetup", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*IsTest", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpPost", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpGet", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpPatch", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpPost", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*HttpDelete", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*Future\(callout\=true\)", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*Future", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*RemoteAction", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*Deprecated", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*InvocableMethod", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*InvocableVariable", TokenType.Attrubute),
                new TokenDefinition(@"(?i)@\s*ReadOnly", TokenType.Attrubute),

                new TokenDefinition(@";", TokenType.StatementTerminator),

                ////new TokenDefinition(@"<(.*?)>", TokenType.ClassNameGeneric),
                ////new TokenDefinition(@"\bList<\b(.*?)>", TokenType.ClassNameGeneric),
                ////new TokenDefinition(@"\Set<\b(.*?)>", TokenType.ClassNameGeneric),
                ////new TokenDefinition(@"\bMap<\b(.*?)>", TokenType.ClassNameGeneric),

                new TokenDefinition(@"\+\+", TokenType.Equal),
                new TokenDefinition(@"\=\=", TokenType.Equal),
                new TokenDefinition(@"\&\&", TokenType.Equal),
                new TokenDefinition(@"\!=", TokenType.Equal),
                new TokenDefinition(@"\=", TokenType.Equal),
                new TokenDefinition(@"\,", TokenType.Comma),
                new TokenDefinition(@":", TokenType.Colon),

                new TokenDefinition(@"[-+]?[0-9]*\.?[0-9]+", TokenType.Decimel),
                new TokenDefinition(@"(?<![-.])\b[0-9]+\b(?!\.[0-9])", TokenType.Integer),

                new TokenDefinition(@"[_A-Za-z0-9!]+", TokenType.Word),

                new TokenDefinition(@"\.", TokenType.Dot),

                new TokenDefinition(@"\r\n", TokenType.Return),
                new TokenDefinition(@"\n", TokenType.Return),

                new TokenDefinition(@"\{", TokenType.OpenCurlyBrackets),
                new TokenDefinition(@"\}", TokenType.CloseCurlyBrackets),

                ////new TokenDefinition(@"\(\)", TokenType.EmptyBrackets),
                new TokenDefinition(@"\(", TokenType.OpenBrackets),
                new TokenDefinition(@"\)", TokenType.CloseBrackets),

                new TokenDefinition(@"\s", TokenType.Space),

                new TokenDefinition(@"\#|\^|\?|\`|\'|\-|\""|\(|\)|\[|\]|\=|\/|\+|\~|\<|\&|\%|\$|\*|\\|\|",
                    TokenType.Anything)
            };

            return regExList;
        }
    }
}
