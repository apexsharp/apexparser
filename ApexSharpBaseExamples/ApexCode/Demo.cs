using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpBaseExamples.ApexCode
{
    public class Demo
    {
        public void MethodOne(int x)
        {
            if (x == 5)
            {
                Console.WriteLine(1);

                if (x == 8)
                {
                    Console.WriteLine(8);
                }

                Console.WriteLine(2);
                Console.WriteLine(3);
            } else if (x == 6)
            {
                Console.WriteLine(6);
            }
            else
            {
                Console.WriteLine(7);
            }
        }
    }
}

