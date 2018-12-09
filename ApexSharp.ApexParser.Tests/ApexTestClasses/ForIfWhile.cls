public with sharing class ForIfWhile
{
    public void MethodIfClean()
    {
        String medal_color ;
        Integer place ;
        if (place == 1)
        {
            medal_color = 'gold' ;
        }
        else if (place == 2)
        {
            medal_color = 'silver' ;
        }
        else if (place == 3)
        {
            medal_color = 'bronze' ;
        }
        else
        {
            medal_color = null ;
        }
    }
    public void MethodForTraditional()
    {
        for (Integer i = 0, j = 0;
                i < 10;
                i++)
        {
            System.debug (i + 1) ;
        }
    }
    public void MethodForIteration()
    {
        Integer[] myInts = new Integer[5] ;
        for (Integer i : myInts)
        {
            System.debug (i) ;
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
}