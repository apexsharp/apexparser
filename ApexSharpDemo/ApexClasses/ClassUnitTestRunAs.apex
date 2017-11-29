@isTest
public class ClassUnitTestRunAs
{
    @isTest
    static void RunAsExample()
    {
        User newUser = new User();
        System.RunAs(newUser)
        {

        }
    }
}