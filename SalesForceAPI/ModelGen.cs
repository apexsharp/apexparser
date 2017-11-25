using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Colorful;
using Newtonsoft.Json;
using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class ModelGen
    {
        public static List<string> SalesForceDefaultFieldTyps = new List<string>
        {
            "string",
            "time",
            "boolean",
            "int",
            "double",
            "date",
            "datetime",
            "base64",
            "id",
            "reference",
            "currency",
            "textarea",
            "percent",
            "phone",
            "url",
            "email",
            "combobox",
            "picklist",
            "multipicklist",
            "anytype",
            "location",
            "address"
        };

        public void CreateOfflineSymbolTable(string sObjectName, string dirPath)
        {


            HttpManager httpManager = new HttpManager();
            var json = httpManager.Get($"sobjects/");

            File.WriteAllText(dirPath + @"\objectList.json", json);


            var sObjectDescribeJson = File.ReadAllText(dirPath + @"\objectList.json");
            SObjectDescribe sObjectList = JsonConvert.DeserializeObject<SObjectDescribe>(sObjectDescribeJson);
            List<Sobject> allSobjects = sObjectList.sobjects.ToList();


            var customObjectCount = allSobjects.Count(x => x.custom);
            var customSetting = allSobjects.Count(x => x.customSetting);
            var objectCount = allSobjects.Count(x => x.custom == false && x.customSetting == false);

            System.Console.WriteLine(customObjectCount);
            System.Console.WriteLine(customSetting);
            System.Console.WriteLine(objectCount);


            List<Sobject> objectsToGet = allSobjects.Where(x => x.name == sObjectName).ToList();


            while (objectsToGet.Count(x => x.downloaded == false) > 0)
            {
                Sobject objectToDownload = objectsToGet.FirstOrDefault(x => x.downloaded == false);
                Console.WriteLine(objectToDownload.name);

                var objectToDownloadList = GetObjectData(objectToDownload, dirPath);
                objectToDownload.downloaded = true;

                var objectsToDownload = allSobjects.Where(x => objectToDownloadList.Contains(x.name));
                objectsToGet.AddRange(objectsToDownload);
            }
        }

        private List<string> GetObjectData(Sobject sobject, string dirPath)
        {
            HttpManager httpManager = new HttpManager();
            var json = httpManager.Get($"sobjects/{sobject.name}/describe");


            File.WriteAllText(dirPath + @"\SObjectJson\" + sobject.name + ".json", json);

            SObjectDetail sObjectDetail = JsonConvert.DeserializeObject<SObjectDetail>(json);

            List<string> objectToDownloadList = CreateSalesForceClasses(sObjectDetail, dirPath);

            //   ConnectionUtil.AddCShaprFile("SObjects", sObjectDetail.name + ".cs");

            return objectToDownloadList;
        }

        private List<string> CreateSalesForceClasses(SObjectDetail objectDetail, string dirPath)
        {
            List<string> objectsToDownload = new List<string>();

            var sb = new StringBuilder();

            sb.AppendLine("namespace PrivateDemo.SObjects");
            sb.AppendLine("{");
            sb.AppendLine("using Apex.System;");
            sb.AppendLine("using SalesForceAPI.ApexApi;");

            sb.AppendLine("\tpublic class " + objectDetail.name + " : SObject");
            sb.AppendLine("\t{");


            foreach (var field in objectDetail.fields)
            {
                if ((field.type == "reference") && (field.name == "OwnerId") && (field.referenceTo.Length > 1))
                {
                    sb.Append("\t\t").Append($"public string {field.name} ").AppendLine("{set;get;}");

                    sb.Append("\t\t").Append($"public {field.referenceTo[1]} {field.relationshipName} ").AppendLine("{set;get;}");
                }
                else if (field.type == "reference" && field.referenceTo.Length > 0)
                {
                    sb.Append("\t\t").Append($"public string {field.name} ").AppendLine("{set;get;}");

                    if (field.relationshipName != null)
                    {
                        sb.Append("\t\t").Append($"public {field.referenceTo[0]} {field.relationshipName} ")
                            .AppendLine("{set;get;}");
                        objectsToDownload.Add(field.referenceTo[0]);
                    }
                }
                else if (field.type != "Id")
                {
                    sb.Append("\t\t").Append($"public {GetField(objectDetail.name, field)} {field.name} ")
                        .AppendLine("{set;get;}");
                }
            }


            sb.AppendLine("\t}");
            sb.AppendLine("}");

            File.WriteAllText(dirPath + @"\SObjects\" + objectDetail.name + ".cs", sb.ToString());
            return objectsToDownload;
        }

        private string GetField(string objectName, Field salesForceField)
        {
            if (SalesForceDefaultFieldTyps.Contains(salesForceField.type))
            {
                switch (salesForceField.type)
                {
                    case "address":
                        return "Address";
                    case "id":
                    case "string":
                    case "picklist":
                    case "email":
                    case "textarea":
                    case "phone":
                    case "url":
                        return "string";
                    case "boolean":
                        return "bool";
                    case "datetime":
                    case "time":
                    case "date":
                        return "DateTime";
                    case "currency":
                    case "percent":
                    case "double":
                        return "double";
                    case "int":
                        return "int";
                }
            }

            Console.WriteLine(
                $"Object Name: {objectName} Field Type: {salesForceField.type} Field Name : {salesForceField.name} Field Length: {salesForceField.length}",
                Color.Red);
            return "string";
        }

        public void CreateServiceStackClasses(SObjectDetail objectDetail)
        {
            using (StreamWriter sw = File.AppendText(@"C:\DevSharp\ApexLib\DbORM\Text\db.txt"))
            {
                sw.WriteLine($"db.DropAndCreateTable<{objectDetail.name}>();");
            }

            using (StreamWriter drop = File.AppendText(@"C:\DevSharp\ApexLib\DbORM\Text\drop.txt"))
            {
                using (StreamWriter sw = File.AppendText(@"C:\DevSharp\ApexLib\DbORM\Text\alter.txt"))
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("using System;");
                    sb.AppendLine("using ServiceStack.DataAnnotations;");
                    sb.AppendLine();

                    sb.AppendLine("namespace DbORM.ModelServiceStack");
                    sb.AppendLine("{");
                    sb.AppendLine("\tpublic class " + objectDetail.name);
                    sb.AppendLine("\t{");


                    foreach (var field in objectDetail.fields)
                    {
                        if ((field.type == "reference") && (field.name == "OwnerId") && (field.referenceTo.Length > 1))
                        {
                            sw.WriteLine(
                                $"Console.WriteLine(db.ExecuteSql(\"ALTER TABLE [dbo].[{objectDetail.name}] ADD CONSTRAINT [{objectDetail.name}#{field.name}#{field.referenceTo[1]}] FOREIGN KEY ({field.name}) REFERENCES [dbo].[{field.referenceTo[1]}] (Id)\"));");
                            drop.WriteLine(
                                $"Console.WriteLine(db.ExecuteSql(\"ALTER TABLE [dbo].[{objectDetail.name}] DROP CONSTRAINT [{objectDetail.name}#{field.name}#{field.referenceTo[1]}]\"));");

                            sb.Append("\t\t").AppendLine($"[StringLength(18)]");
                            sb.Append("\t\t")
                                .AppendLine(
                                    $"[ForeignKey(typeof({field.referenceTo[1]}), ForeignKeyName=\"FK_{objectDetail.name}#{field.name}#{field.referenceTo[1]}\")]");
                            sb.Append("\t\t").Append($"public string {field.name} ").AppendLine("{set;get;}");
                            sb.Append("\t\t").AppendLine($"[Ignore]");
                            sb.Append("\t\t").Append($"public {field.referenceTo[1]} {field.relationshipName} ")
                                .AppendLine("{set;get;}");
                        }
                        else if (field.type == "reference" && field.referenceTo.Length > 0)
                        {
                            sw.WriteLine(
                                $"Console.WriteLine(db.ExecuteSql(\"ALTER TABLE [dbo].[{objectDetail.name}] ADD CONSTRAINT [{objectDetail.name}#{field.name}#{field.referenceTo[0]}] FOREIGN KEY ({field.name}) REFERENCES [dbo].[{field.referenceTo[0]}] (Id)\"));");
                            drop.WriteLine(
                                $"Console.WriteLine(db.ExecuteSql(\"ALTER TABLE [dbo].[{objectDetail.name}] DROP CONSTRAINT [{objectDetail.name}#{field.name}#{field.referenceTo[0]}]\"));");

                            sb.Append("\t\t").AppendLine($"[StringLength(18)]");
                            sb.Append("\t\t")
                                .AppendLine(
                                    $"[ForeignKey(typeof({field.referenceTo[0]}), ForeignKeyName=\"FK_{objectDetail.name}#{field.name}#{field.referenceTo[0]}\")]");
                            sb.Append("\t\t").Append($"public string {field.name} ").AppendLine("{set;get;}");


                            if (field.relationshipName != null)
                            {
                                sb.Append("\t\t").AppendLine($"[Ignore]");
                                sb.Append("\t\t").Append($"public {field.referenceTo[0]} {field.relationshipName} ")
                                    .AppendLine("{set;get;}");
                            }
                        }
                        else if (field.name == "Id")
                        {
                            sb.Append("\t\t").AppendLine($"[Index(Unique=true)]");
                            sb.Append("\t\t").AppendLine($"[StringLength(18)]");
                            sb.Append("\t\t").Append($"public string {field.name} ").AppendLine("{set;get;}");
                        }
                        else
                        {
                            if (field.length > 0)
                            {
                                sb.Append("\t\t").AppendLine($"[StringLength({field.length})]");
                            }

                            sb.Append("\t\t").Append($"public {GetField(objectDetail.name, field)} {field.name} ")
                                .AppendLine("{set;get;}");
                        }
                    }


                    sb.AppendLine("\t}");
                    sb.AppendLine("}");

                    File.WriteAllText(@"C:\DevSharp\ApexLib\DbORM\ModelServiceStack\" + objectDetail.name + ".cs",
                        sb.ToString());
                }
            }
        }
    }
}