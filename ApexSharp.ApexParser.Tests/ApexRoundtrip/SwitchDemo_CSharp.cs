namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    public class SwitchDemo
    {
        public static void Method()
        {
            int x = 123;
            switch (x)
            {
            	case 5:
            	case 6:
            	case 7:
            		System.debug("Cool!"); // 1
            		break;

            	case string c:
            		switch (c)
            		{
            			// 2
            			case "foo":
            				System.debug("bar"); // 3
            				break;

            			default:
            				System.debug("baz"); // 4
            			    break; // 5
            		}

        		    System.debug("corge");
            	    break; // 6

            	default:
            		return; // 7
            }

            while (true)
            {
            	System.debug("quux"); // 8
            }
        }
    }
}
