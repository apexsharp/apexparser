using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SalesForceAPI.Model.RestApi;
using Serilog;
using Console = Colorful.Console;

namespace SalesForceAPI
{
    public class SObjectCode
    {
        public string ClassName { set; get; }
        public string Code { set; get; }
    }
    public class ModelGen
    {
        readonly HashSet<string> _objectsToGet = new HashSet<string>();
        readonly HashSet<string> _objectsWeGot = new HashSet<string>();

        private List<Sobject> _sObjectListJson = new List<Sobject>();

        public List<Sobject> GetAllObjects()
        {
            if (_sObjectListJson.Any())
            {
                return _sObjectListJson;
            }

            string dirPath = ConnectionUtil.GetSession().CatchLocation.FullName;

            HttpManager httpManager = new HttpManager();
            var requestJson = httpManager.Get($"sobjects/");
            File.WriteAllText(dirPath + @"\objectList.json", requestJson);

            var json = File.ReadAllText(dirPath + @"\objectList.json");
            SObjectDescribe sObjectList = JsonConvert.DeserializeObject<SObjectDescribe>(json);
            _sObjectListJson = sObjectList.sobjects.ToList();

            //var customObjectCount = allSobjects.Where(x => x.custom).ToList();
            //var customSetting = allSobjects.Where(x => x.customSetting).ToList();
            //var objectCount = allSobjects.Where(x => x.custom == false && x.customSetting == false).ToList();

            //System.Console.WriteLine(customObjectCount.Count());
            //System.Console.WriteLine(customSetting.Count());
            //System.Console.WriteLine(objectCount.Count());

            return _sObjectListJson;
        }

        public List<SObjectCode> CreateOfflineSymbolTable(string nameSpace, params string[] objectNameList)
        {

            List<SObjectCode> codeList = new List<SObjectCode>();

            var allSobjects = GetAllObjects();

            // Double check if the object exists, if not error
            foreach (var objectToRead in objectNameList)
            {
                if (allSobjects.Any(x => x.name == objectToRead))
                {
                    _objectsToGet.Add(allSobjects.First(x => x.name == objectToRead).name);
                }
                else
                {
                    Log.ForContext<ModelGen>().Error("Object {@name} Not Found", objectToRead);
                }
            }

            while (_objectsToGet.Count > 0)
            {
                var objectToDownload = _objectsToGet.First();
                HttpManager httpManager = new HttpManager();
                var objectDetailjson = httpManager.Get($"sobjects/{objectToDownload}/describe");

                SObjectDetail sObjectDetail = JsonConvert.DeserializeObject<SObjectDetail>(objectDetailjson);

                string dirPath = ConnectionUtil.GetSession().CatchLocation.FullName;
                objectDetailjson = JsonConvert.SerializeObject(sObjectDetail, Formatting.Indented);
                File.WriteAllText(dirPath + $"\\{objectToDownload}.json", objectDetailjson);


                _objectsWeGot.Add(objectToDownload);
                var sobjectcode = new SObjectCode
                {
                    ClassName = $"{objectToDownload}.cs",
                    Code = CreateSalesForceClasses(sObjectDetail, nameSpace)
                };
                _objectsToGet.Remove(objectToDownload);

                codeList.Add(sobjectcode);
            }
            return codeList;
        }

        private string CreateSalesForceClasses(SObjectDetail objectDetail, string nameSpace)
        {
            var sb = new StringBuilder();

            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");
            sb.AppendLine("\tusing Apex.System;");
            sb.AppendLine("\tusing SalesForceAPI.ApexApi;");
            sb.AppendLine();
            sb.AppendLine($"\tpublic class {objectDetail.name} : SObject");
            sb.AppendLine("\t{");

            var setGet = "{set;get;}";
            foreach (var objectField in objectDetail.fields)
            {
                if ((objectField.type == "reference") && (objectField.name == "OwnerId") && (objectField.referenceTo.Length > 1))
                {
                    sb.AppendLine($"\t\tpublic string {objectField.name} {setGet}");

                    sb.AppendLine($"\t\tpublic {objectField.referenceTo[1]} {objectField.relationshipName} {setGet}");
                }
                else if (objectField.type == "reference" && objectField.referenceTo.Length > 0)
                {
                    sb.AppendLine($"\t\tpublic string {objectField.name} {setGet}");

                    if (objectField.relationshipName != null)
                    {
                        sb.AppendLine($"\t\tpublic {objectField.referenceTo[0]} {objectField.relationshipName} {setGet}");

                        foreach (var refferenceObject in objectField.referenceTo)
                        {
                            if (_objectsWeGot.Contains(refferenceObject))
                            {
                                // Don't add if we have downloaded this object
                            }
                            else
                            {
                                _objectsToGet.Add(refferenceObject);
                            }
                        }
                    }
                }
                else if (objectField.type != "id")
                {
                    sb.AppendLine($"\t\tpublic {GetField(objectField)} {objectField.name} {setGet}");
                }
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();

        }

        private string GetField(Field salesForceField)
        {
            var valueFound = FieldDictionary.TryGetValue(salesForceField.type, out var value);
            if (valueFound)
            {
                return value;
            }
            Console.WriteLine($"Field Type: {salesForceField.type} Field Name : {salesForceField.name} Field Length: {salesForceField.length}", Color.Red);
            return "NOT FOUND";
        }

        public static Dictionary<string, string> FieldDictionary = new Dictionary<string, string>()
        {
            {"address", "Address" },
            {"id","Id" },
            {"string","string" },
            {"picklist","string" },
            {"email","string" },
            {"textarea","string" },
            {"phone","string" },
            {"url","string" },
            {"reference","string" },
            {"combobox","string" },
            {"multipicklist","string" },
            {"anytype","object" },
            {"location","string" },
            {"boolean","bool" },
            {"datetime","DateTime" },
            {"time","DateTime" },
            {"date","DateTime" },
            {"currency","double" },
            {"percent","double" },
            {"double","double" },
            {"int","int" }
        };
    }
}