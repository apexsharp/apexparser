namespace ApexSharpDemo.ApexCode
{
    using Apex.System;

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
                System.Debug(i);
            }
        }
        public void MethodForIteration()
        {
            int[] myInts = new int[5];
            foreach (var myInt in myInts)
            {
                System.Debug(myInt);
            }

        }
        public void MethodDo()
        {
            int count = 1;
            do
            {
                System.Debug(count);
                count++;
            }
            while (count < 11);
        }
        public void MethodWhile()
        {
            int count = 1;
            while (count < 11)
            {
                System.Debug(count);
                count++;
            }
        }
    }
}
