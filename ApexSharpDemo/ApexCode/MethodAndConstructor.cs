using Apex.ApexAttrbutes;

namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public abstract class MethodAndConstructor
    {
        public abstract void MethodThree();

        MethodAndConstructor()
        {
        }

        public MethodAndConstructor(string demo)
        {
        }

        public virtual void VirtualMethod()
        {
        }

        [Future]
        public static void FutureMethod()
        {
        }

        [Future(callOut: true)]
        public static void FutureMethodWithCallOut()
        {
        }

        void StringVoid()
        {
        }

        private void StringPrivateVoid()
        {
        }

        public void StringPublic()
        {
        }

        string GetString()
        {
            return "Hello World";
        }

        public string GetStringPublic()
        {
            return "Hello World";
        }

        private string GetStringprivate()
        {
            return "Hello World";
        }

        public string GetStringglobal()
        {
            return "Hello World";
        }
    }
}
