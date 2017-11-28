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
    @future
    public static void FutureMethod()
    {
    }
    @future(callOut=true)
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
        return 'Hello World';
    }
    public string GetStringPublic()
    {
        return 'Hello World';
    }
    private string GetStringprivate()
    {
        return 'Hello World';
    }
    public string GetStringglobal()
    {
        return 'Hello World';
    }

    List<string> GetGenericList()
    {
        return new List<string>();
    }
    Map<string, string> GetGenericMap()
    {
        return new Map<string, string>();
    }
    void arrayMethodTwoPrivate(List<string> stringArray)
    {
    }

    public static Map<string, string> arrayMethodOne(Map<string, string> stringArray, List<string> stringArrayList)
    {
        return null;
    }
    public static void arrayMethodTwo(List<string> stringArray)
    {
    }

    public Map<string, string> methodReturnMap(Map<string, string> mapList)
    {
        return null;
    }
}