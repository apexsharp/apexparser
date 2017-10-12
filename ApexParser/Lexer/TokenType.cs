namespace ApexParser.Lexer
{
    public enum TokenType
    {
        ReturnType,
        Empty,
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
        StatementTerminator,
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
}