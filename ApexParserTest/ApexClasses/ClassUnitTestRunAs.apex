@isTest
public class ClassUnitTestRunAs
{
    static testMethod void RunAsExample()
    {
        User newUser = new User();
        System.RunAs(newUser)
        {

        }
    }
}