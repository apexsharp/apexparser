using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;

namespace PrivateDemo
{
    public class FileWatcher
    {
        public static void FileWatcherStart()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = @"C:\DevSharp\out\";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Renamed += OnRenamed;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
            Console.ReadKey();
        }

        public static int LastFileWrite = 0;

        private static void OnChanged(object source, FileSystemEventArgs eventArg)
        {
            if (eventArg.Name.EndsWith("cls"))
            {
                int lastWrite = File.GetLastWriteTime(@"C:\DevSharp\out\" + eventArg.Name).Second;

                if (LastFileWrite != lastWrite)
                {
                    LastFileWrite = lastWrite;
                    try
                    {
                        Console.WriteLine("Parsing " + eventArg.Name + "  " + DateTime.Now);

                        using (var fileStream = new FileStream(@"C:\DevSharp\out\" + eventArg.Name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var textReader = new StreamReader(fileStream))
                        {

                        }


                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
        }

        private static void OnRenamed(object source, RenamedEventArgs ex)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(ex.ChangeType);
        }
    }
}

