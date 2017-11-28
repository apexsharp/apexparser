namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class ForIfWhile
    {
        public void MethodIfClean(int place)
        {
            string modelColor;
            if (place == 1)
            {
                modelColor = "Gold";
            }
            else if (place == 2)
            {
                modelColor = "Silver";
            }
            else if (place == 3)
            {
                modelColor = "Bronze";
            }
            else
            {
                modelColor = null;
            }
        }

        public void MethodForTraditional()
        {
            for (int i = 0; i < 10; i++)
            {
                System.debug (i + 1);
            }
        }

        public void MethodForIteration()
        {
            int[] myInts = new int[5];
            foreach (int myInt in myInts)
            {
                System.debug (myInt);
            }
        }

        public void MethodDo()
        {
            int count = 1;
            do
            {
                System.debug (count);
                count++;
            }
            while (count < 11);
        }

        public void MethodWhile()
        {
            int count = 1;
            while (count < 11)
            {
                System.debug (count);
                count++;
            }
        }

        public void ForLoopTest()
        {
            for (int i = 0; i < 10; i++)
            {
                System.debug (i + 1);
            }
        }

        // Nested If
        public static string GetContact(string nameString)
        {
            if (nameString.Length()> 0)
            {
                if (nameString.Length()== 1)
                {
                    return "Gold";
                }
                else if (nameString.Length()== 2)
                {
                    return "Silver";
                }
                else
                {
                    return "Nothing";
                }
            }
            else if (nameString.Length()> 0)
            {
                return nameString;
            }
            else
            {
                return "";
            }
        }

        public static void ForSoql()
        {
            foreach (Contact contactList in Soql.Query<Contact>("SELECT Id, Name FROM Contact"))
            {
            }
        }
    }
}
