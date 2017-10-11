namespace ApexSharpBase.MetaClass
{
    public enum SyntaxType
    {
        NotFound,
        ClassContainer,
        Class,
        Constructor,
        Method,



        Expression,
        LocalDeclaration,
        FieldDeclaration,
        PropertyDeclaration,
        AccessorDeclaration,
        ForStatement,
        ForEachStatement,
        ReturnStatement,

        IfStatement,
        ElseStatement,

        TryStatementSyntax,
        CatchClauseSyntax,
        FinallyClauseSyntax,
        ThrownStatementSyntax,

        InvocationExpression,

        WhileStatementSyntax,
        DoStatementSyntax,


        Soql,
        Dml,
        CloseBrace,
        OpenBrance,

    }
}