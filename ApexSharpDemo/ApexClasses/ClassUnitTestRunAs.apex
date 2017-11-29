@isTest
public class ClassUnitTestRunAs
{
    @isTest
    static void RunAsExample()
    {
        User newUser = [SELECT Id FROM User LIMIT 1];
        System.RunAs(newUser)
        {

        }
    }
}