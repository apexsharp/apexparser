public class ForIfWhile
{
    public void MethodIfClean(Integer place)
    {
        String modelColor;

        if (place == 1)
        {
            modelColor = 'Gold' ;
        }
        else if (place == 2)
        {
            modelColor = 'Silver' ;
        }
        else if (place == 3)
        {
            modelColor = 'Bronze' ;
        }
        else
        {
            modelColor = null ;
        }
    }
    public void MethodForTraditional()
    {
        for (Integer i = 0; i < 10; i++)
        {
            System.debug (i + 1) ;
        }
    }
    public void MethodForIteration()
    {
        Integer[] myInts = new Integer[5] ;
        for (Integer myInt : myInts)
        {
            System.debug (myInt) ;
        }
    }
    public void MethodDo()
    {
        Integer count = 1 ;
        do
        {
            System.debug (count) ;
            count++ ;
        }
        while (count < 11) ;
    }
    public void MethodWhile()
    {
        Integer count = 1 ;
        while (count < 11)
        {
            System.debug (count) ;
            count++ ;
        }
    }

    public void ForLoopTest()
    {
        for (Integer i = 0; i < 10; i++)
        {
            System.debug (i + 1);
        }
    }

    // Nested If
    public static string GetContact(String nameString)
    {
        if (nameString.Length() > 0)
        {
            if (nameString.Length() == 1)
            {
                return 'Gold';
            }
            else if (nameString.Length() == 2)
            {
                return 'Silver';
            } else {
                return 'Nothing';
            }
        }
        else if (nameString.Length() > 0)
        {
            return nameString;
        }
        else
        {
            return '';
        }
    }

    public static void ForSoql()
    {
        for(Contact contactList : [SELECT Id, Name FROM Contact]) {

        }
    }
}