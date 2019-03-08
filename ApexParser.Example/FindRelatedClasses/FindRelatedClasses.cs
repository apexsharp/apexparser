using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApexSharpDemo.FindRelatedClasses
{
    public static class CollectionExtensions
    {
        public static void AddItem<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static void AddItem<T>(this Stack<T> stack, T item)
        {
            if (!stack.Contains(item))
            {
                stack.Push(item);
            }
        }
    }

    public class ApexRelatedClass
    {
        public string ApexClassName { get; set; }

        // The list of Apex classes called by the above named class
        public List<ApexRelatedClass> CalledApexClass { get; set; }
    }

    // Given a class name, list all apex classes the give class name calls and also the classes those class call, recursive.
    // This is use full when you want to create Packaging 2.0
    public class FindRelatedClasses
    {
        public static List<FileInfo> GetFilesAsFileInfo(DirectoryInfo dir, String dirFilter)
        {
            List<FileInfo> apexFileNameList = new List<FileInfo>();

            var apexFileList = Directory.GetFiles(dir.FullName, dirFilter , SearchOption.TopDirectoryOnly).ToList();
            foreach (var apexFile in apexFileList)
            {
                FileInfo apexFileInfo = new FileInfo(apexFile);
                apexFileNameList.Add(apexFileInfo);
            }

            return apexFileNameList;
        }

        public static ApexRelatedClass GetAllRealatedApexClasses(DirectoryInfo apexDir, FileInfo rootApexClassName)
        {
            return new ApexRelatedClass();
        }

        public static List<FileInfo> GetAllRealatedApexFiles(DirectoryInfo apexDir, FileInfo rootApexClassName)
        {
            Stack<FileInfo> readFileNames = new Stack<FileInfo>();
            readFileNames.Push(rootApexClassName);

            List<FileInfo> apexFileFound = new List<FileInfo> {rootApexClassName};

            while (readFileNames.Count > 0)
            {
                // Need the Apex Class with out the .cls
                var apexClassName = readFileNames.Pop().Name.Replace(".cls", "");

                var apexFileList = GetFilesAsFileInfo(apexDir, "*.cls");
                foreach (var apexFile in apexFileList)
                {
                    var rootFile = File.ReadAllText(apexFile.FullName);

                    if (RelatedClassHelper.IsRelated(rootFile, apexClassName))
                    {
                        apexFileFound.AddItem(apexFile);

                        // Only push files we have not looked at.
                        if (apexFileFound.Contains((FileInfo) apexFile) == false)
                        {
                           readFileNames.Push(apexFile);
                        }
                    }
                }
            }

            return apexFileFound;
        }
    }
}
