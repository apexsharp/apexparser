public class ExceptionDemo
{
    public static void CatchDemo()
    {
        try
        {
            ThrowDemo();
        }
        catch (MathException e)
        {
            System.debug(e.getMessage());
        }
        finally
        {
            System.debug('Finally');
        }
    }

    public static void ThrowDemo()
    {
        throw new MathException('something bad happened!');
    }
}