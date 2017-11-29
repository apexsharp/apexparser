@isTest
public class ClassUnitTestRunAs
{
    public static testMethod void RunAsExample()
    {
        User newUser = new User();
        System.RunAs(newUser)
        {

        }
    }
}