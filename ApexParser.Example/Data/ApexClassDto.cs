namespace ApexSharpDemo.Data
{

    public class ApexClassDto
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public object[] constructors { get; set; }
        public Method[] methods { get; set; }
        public object[] properties { get; set; }
    }

    public class Method
    {
        public string[] argTypes { get; set; }
        public bool isStatic { get; set; }
        public object methodDoc { get; set; }
        public string name { get; set; }
        public Parameter[] parameters { get; set; }
        public object[] references { get; set; }
        public string returnType { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public string type { get; set; }
    }

}
