@isTest
public class ClassUnitTest
{
    @TestSetup
    public static void Setup()
    {
        System.Debug('One Time Setup Got Called');
    }
    @isTest
    public static void AssertTrue()
    {
        System.Assert(true, 'Assert True');
    }
    @isTest
    public static void AssertEquals()
    {
        System.AssertEquals(5, 5, 'Assert Equal');
    }
    @isTest
    public static void AssertNotEquals()
    {
        System.AssertNotEquals(5, 0, 'Assert Not Equal');
    }
    testMethod public static void AssertNew()
    {
        System.Assert(true, 'Assert True');
    }
    static testMethod public void AssertEqualsNew()
    {
        System.AssertEquals(5, 5, 'Assert Equal');
    }
    static public testMethod void AssertNotEqualsNew()
    {
        System.AssertNotEquals(5, 0, 'Assert Not Equal');
    }
}