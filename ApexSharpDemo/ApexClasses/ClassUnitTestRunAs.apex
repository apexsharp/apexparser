@isTest
public class ClassUnitTestRunAs
{
    @isTest
    public static void RunAsExample()
    {
        User newUser = new User();
        System.RunAs(newUser)
        {

        }
    }
}