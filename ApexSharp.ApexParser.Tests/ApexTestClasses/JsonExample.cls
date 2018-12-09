public class JsonExample
{
    public void JsonExampleMethod()
    {
        String name = 'Jay';

        String jsonString = JSON.Serialize(name);

        String newName = (String)JSON.Deserialize(jsonString, String.class);
    }
}