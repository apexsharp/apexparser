public class ForIfWhile
{
    public void MethodIfClean(Integer place)
    {
        // string model color
        String modelColor;

        // if place equals 1
        if (place == 1)
        {
            modelColor = 'Gold';
        }
        else if (place == 2)
        {
            modelColor = 'Silver';
        }
        else if (place == 3)
        {
            modelColor = 'Bronze';
        }
        else
        {
            modelColor = null;
        } // else
    }

    public void MethodForTraditional()
    {
        // for integer
        for (Integer i = 0; i < 10; i++)
        {
            // system debug
            System.debug (i + 1); // debug
        }
    }

    public void MethodForIteration()
    {
        // integer myints
        Integer[] myInts = new Integer[5];
        for (Integer myInt : myInts)
        {
            System.debug (myInt);
        } // for
    }

    public void MethodDo()
    {
        // integer count
        Integer count = 1;

        // do while
        do
        {
            System.debug (count);

            // increase count
            count++;
        }
        while (count < 11); // while
    }

    public void MethodWhile()
    {
        Integer count = 1;

        // while count
        while (count < 11)
        {
            System.debug (count);
            count++;
        } // while
    }

    public void ForLoopTest()
    {
        // for integer = 0
        for (Integer i = 0; i < 10; i++)
        {
            System.debug (i + 1);
        } // for
    }

    // Nested If
    public static String GetContact(String nameString)
    {
        // outer if
        if (nameString.Length()> 0)
        {
            // inner if
            if (nameString.Length()== 1)
            {
                return 'Gold'; // return
            }
            else if (nameString.Length()== 2)
            {
                return 'Silver';
            }
            else
            {
                return 'Nothing';
            } // inner
        }
        else if (nameString.Length()> 0)
        {
            return nameString;
        }
        else
        {
            return '';
        } // outer
    }

    public static void ForSoql()
    {
        // for soql
        for (Contact contactList : [SELECT Id, Name FROM Contact])
        {
            // do nothing
        }
    }
}
