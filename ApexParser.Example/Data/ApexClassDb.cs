using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using String = Apex.System.String;

namespace ApexSharpDemo.Data
{
    public class ApexMethodDto
    {
        public string NameSpace { get; set; }
        public String ClassName { get; set; }
        public String MethodName { get; set; }
        public String ParameterList { get; set; }
    }
    public class ApexClassDb
    {
        private static List<ApexClassDto> _apexClassList = new List<ApexClassDto>();


        public static void Main(string[] args)
        {

            // String jsonOne = JSON.serialize(objectToSerialize);
            // String jsonOne = JSON.serialize(objectToSerialize, suppressApexObjectNulls)
            List<string> classNames = new List<string>();
            ApexClassDb db = new ApexClassDb();
            List<ApexClassDto> apexClasses = db.GetApexClassesDb();

            foreach (var apexClass in apexClasses)
            {
                foreach (Method method in apexClass.methods)
                {
                    List<string> parameters = new List<string>();
                    for (int i = 0; i < method.parameters.Length; i++)
                    {
                        method.parameters[i].type = method.parameters[i].type.Replace(',', '.');
                        parameters.Add(method.parameters[i].type);
                    }

                    var allParameters = string.Join(":", parameters);

                    method.returnType = method.returnType.Replace(',', '.');
                    classNames.Add($"{method.returnType},{apexClass.NameSpace},{apexClass.ClassName},{method.name},{allParameters}");
                }
            }

            string apexClassFileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            File.WriteAllLines(apexClassFileLocation + @"\Data\SalesForceClasses.csv", classNames);


            //Console.WriteLine("Done, Press Any Key To Exit");
            //Console.ReadLine();

        }

        public ApexClassDb()
        {
            _apexClassList = GetApexClassesDb();
        }

        public ApexMethodDto ValidateApexMethod(ApexMethodDto apexMethodMethod)
        {

            return new ApexMethodDto();
        }

        public void CreateApexClassDb()
        {
            string apexClassFileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            Console.WriteLine(apexClassFileLocation);

            string apexJson = File.ReadAllText(apexClassFileLocation + @"\Data\AllApexClasses.json");
            JObject apexJObject = JObject.Parse(apexJson);

            List<JToken> nameSpaceList = apexJObject.Children().Children().ToList();
            foreach (var nameSpace in nameSpaceList)
            {
                foreach (var apexClasses in nameSpace.Children().ToList())
                {
                    var apexClassDto = JsonConvert.DeserializeObject<ApexClassDto>(apexClasses.First.ToString());
                    apexClassDto.NameSpace = apexClasses.Path.Split('.')[0];
                    apexClassDto.ClassName = apexClasses.Path.Split('.')[1];

                    _apexClassList.Add(apexClassDto);
                }
            }

            var json = JsonConvert.SerializeObject(_apexClassList, Formatting.Indented);
            File.WriteAllText(apexClassFileLocation + @"\Data\SalesForceClasses.json", json);
        }

        public List<ApexClassDto> GetApexClassesDb()
        {
            string apexClassFileLocation = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            Console.WriteLine(apexClassFileLocation);

            string apexJson = File.ReadAllText(apexClassFileLocation + @"\Data\SalesForceClasses.json");
            _apexClassList = JsonConvert.DeserializeObject<List<ApexClassDto>>(apexJson);
            return _apexClassList;
        }

        public string GetCorrectMethodName(string apexNameSpace, string apexClassName, string apexMethodName)
        {
            if (apexNameSpace.Length == 0)
            {
                apexNameSpace = "System";
            }

            var apexClass = _apexClassList.FirstOrDefault(x => x.NameSpace.Equals(apexNameSpace, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.ClassName.Equals(apexClassName, StringComparison.InvariantCultureIgnoreCase));

            var apexMethod = apexClass?.methods.FirstOrDefault(x =>
                x.name.Equals(apexMethodName, StringComparison.InvariantCultureIgnoreCase));

            return apexMethod?.name;
        }

        public string GetCorrectClassName(string apexNameSpace, string apexClassName)
        {
            if (apexNameSpace.Length == 0)
            {
                apexNameSpace = "System";
            }

            var apexClass = _apexClassList.FirstOrDefault(x => x.NameSpace.Equals(apexNameSpace, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.ClassName.Equals(apexClassName, StringComparison.InvariantCultureIgnoreCase));

            return apexClass?.ClassName;
        }

        public string GetCorrectNameSpace(string apexNameSpace)
        {
            if (apexNameSpace.Length == 0)
            {
                apexNameSpace = "System";
            }

            var apexClass = _apexClassList.FirstOrDefault(x => x.NameSpace.Equals(apexNameSpace, StringComparison.InvariantCultureIgnoreCase));

            return apexClass?.NameSpace;
        }

        public ApexClassDto GetApexClass(string apexNameSpace, string apexClassName)
        {
            if (apexNameSpace.Length == 0)
            {
                apexNameSpace = "System";
            }

            var apexClass = _apexClassList.FirstOrDefault(x => x.NameSpace.Equals(apexNameSpace, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.ClassName.Equals(apexClassName, StringComparison.InvariantCultureIgnoreCase));
            if (apexClass != null)
            {
                if (apexNameSpace == "System")
                {
                    apexClass.NameSpace = System.String.Empty;
                    return apexClass;
                }
                else
                {
                    return apexClass;
                }
            }

            return null;
        }


        public List<Method> GetCorrectMethod(string apexNameSpace, string apexClassName, string apexMethodName)
        {
            if (apexNameSpace.Length == 0)
            {
                apexNameSpace = "System";
            }

            var apexClass = _apexClassList.FirstOrDefault(x => x.NameSpace.Equals(apexNameSpace, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.ClassName.Equals(apexClassName, StringComparison.InvariantCultureIgnoreCase));

            var methodList = apexClass.methods.Where(x => x.name.Equals(apexMethodName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            return methodList;
        }
    }
}
