namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    public class IfElse
    {
        public void SingleIf()
        {
            int qa = 0;

            if (qa == 0)
            {
                System.Debug("Zero");
            }
        }

        public void IfAndElse()
        {
            int qa = 0;

            if (qa == 0)
            {
                System.Debug("Zero");
            }
            else
            {
                System.Debug("Else");
            }
        }

        public void MultipleIfAndElse()
        {
            int qa = 0;

            if (qa == 0)
            {
                System.Debug("Zero");
            }
            else if (qa == 1)
            {
                System.Debug("One");
            }
            else
            {
                System.Debug("Else");
            }
        }
    }
}
