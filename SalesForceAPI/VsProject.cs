using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;

namespace SalesForceAPI
{
    class VsProject
    {

        public void SetupProject()
        {
            // string projectDirectoryName = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            // List<string> cShaprFileList = Directory.GetFileSystemEntries(projectDirectoryName, "*.csproj").ToList();
            //   project = new Microsoft.Build.Evaluation.Project(visualStudioProjFile);
        }

        private void AddDirectory(string dirName)
        {
            var project = new Project(dirName);
            Directory.CreateDirectory(project.DirectoryPath + @"\Model\");
            dirName = dirName + @"\";
            var projectItems = project.GetItems("Folder").ToList();
            if (projectItems.Any(x => x.EvaluatedInclude == dirName) == false)
            {
                project.AddItem("Folder", dirName);
                project.Save();
            }
        }



        //    AddCShaprFile("Model", "Demo.cs");
        public static void AddCShaprFile(string dirName, string fileName)
        {
            var project = new Project(dirName);
            var cSharpFileName = dirName + @"\" + fileName;
            var projectItems = project.GetItems("Compile").ToList();

            if (projectItems.Any(x => x.EvaluatedInclude == cSharpFileName) == false)
            {
                project.AddItem("Compile", cSharpFileName);
                project.Save();
            }
        }

    }
}
