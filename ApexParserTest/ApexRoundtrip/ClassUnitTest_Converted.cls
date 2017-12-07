@IsTest
public class ClassUnitTest
{
    @TestSetup
    public static void Setup()
    {
        System.Debug('One time Setup Got Called');
    }

    @IsTest
    public static void Assert()
    {
        System.Assert(true, 'Assert True');
    }

    @IsTest
    public static void AssertTestMethod()
    {
        System.Assert(true, 'Assert True');
    }

    @IsTest
    public static void AssertEquals()
    {
        System.AssertEquals(5, 5, 'Assert Equal');
    }

    @IsTest
    public static void AssertEqualsTestMethod()
    {
        System.AssertEquals(5, 5, 'Assert Equal');
    }

    @IsTest
    public static void AssertNotEquals()
    {
        System.AssertNotEquals(5, 0, 'Assert Not Equal');
    }

    @IsTest
    public static void AssertNotTestMethod()
    {
        System.AssertNotEquals(5, 0, 'Assert Not Equal');
    }
}
