// ClassDeclaration
public with sharing class PropertyAndField
{
    // PropertyDeclarationSyntax
    public DateTime DateTimeGetSet { get; set; }
    // PropertyDeclarationSyntax
    public List<DateTime> DateTimeGetSetGeneric { get; set; }
    // PropertyDeclarationSyntax
    public DateTime[] DateTimeGetSetArray { get; set; }
    // FieldDeclarationSyntax
    public DateTime DateTimeEmpty;
    // FieldDeclarationSyntax
    public DateTime DateTimeInitilized = DateTime.now();
    // FieldDeclarationSyntax
    public List<DateTime> DateTimeList = new List<DateTime>();
    // FieldDeclarationSyntax
    public DateTime[] DateTimeArrary = new DateTime[5];
    // FieldDeclarationSyntax
    public String Name = 'jay';
    public final String nameFinal = 'jay';
    public static String NameStatic = 'jay';
    public static final String NameStaticFinal = 'jay';

    public void MethodOne()
    {
        //FieldDeclaration
        DateTime dateTimeEmpty;
        //Expression
        dateTimeEmpty = DateTime.now();
        //FieldDeclaration
        DateTime dateTimeInitilized = DateTime.now();
        //FieldDeclaration
        List<DateTime> dateTimeList = new List<DateTime>();
        //FieldDeclaration
        DateTime[] dateTimeArrary = new DateTime[5];
        //ExpressionStatement
        System.debug('Hello World');
        String Name;
        Name = 'Jay';
    }
}