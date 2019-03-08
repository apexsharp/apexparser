namespace ApexSharpDemo.ListClassesAndMethods
{
    public class DemoSetup
    {
        public static void StaticMethod()
        {

        }

        public static DemoSetup StaticMethodTwo()
        {
            return new DemoSetup();
        }

        public void InstanceMethod()
        {

        }
    }
}