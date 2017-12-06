using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SF
{
    class Program
    {
        static void Main(string[] args)
        {
            string buffer = ">";
            ConsoleKeyInfo consoleKey;

            LinkedList<string> linked = new LinkedList<string>();
            LinkedListNode<string> node = linked.AddLast(">Top of History");

            Console.Write(buffer);
            do
            {
                //Console.WriteLine(Console.CursorLeft + " " + Console.CursorTop);
                ConsoleKeyInfo lastConsoleKey = consoleKey;
                consoleKey = Console.ReadKey();

                switch (consoleKey.Key)
                {
                    case ConsoleKey.Q: 
                        {
                            buffer = "";
                            Console.Clear();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                          Console.Clear();
                          buffer = buffer + node.Value;
                          Console.Write(buffer);

                            if (node.Previous != null)
                            {
                                node = node.Previous;
                            }
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (node.Next != null)
                            {                                
                                node = node.Next;

                                Console.Clear();
                                buffer = buffer + node.Value;
                                Console.Write(buffer);
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                buffer = buffer + Environment.NewLine + ">End of History";
                                Console.WriteLine(buffer);
                            }
                            break;
                        }

                    case ConsoleKey.A:
                    case ConsoleKey.R:
                        if(lastConsoleKey.Key == ConsoleKey.P)
                        {
                            Console.Clear();
                            buffer = buffer + "roject:";
                            Console.Write(buffer);
                        }
                        break;
                    case ConsoleKey.H:
                        {
                            Console.WriteLine();
                            foreach (var linkedNode in linked)
                            {
                                Console.WriteLine(linkedNode);

                            }
                            break;
                        }
                    case ConsoleKey.O:
                        {
                            Console.Clear();
                            buffer = buffer + "orgs:";
                            Console.Write(buffer);
                            break;
                        }
                    case ConsoleKey.L:
                        {
                            Console.Clear();
                      
                            buffer = buffer + "list:";
                            Console.Write(buffer);
                            break;
                        }
                    case ConsoleKey.Backspace:
                        {
                            // Are there any characters to erase?
                            if (buffer.Length >= 1)
                            {
                                buffer = buffer.Substring(0, buffer.Length - 1);
                                Console.Clear();
                                Console.Write(buffer);
                            }
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            Console.Clear();
                            var lastVal = buffer.Substring(buffer.Length - 4);

                            LinkedListNode<string> lln = new LinkedListNode<string>(lastVal);
                
                            linked.AddLast(lln);
                            Console.Write(buffer);


                            Console.Clear();
                            buffer = buffer + Environment.NewLine + ExecuteSalesForceDx(buffer);
                            buffer = buffer + Environment.NewLine  + ">";                            
                            Console.Write(buffer);

                            break;
                        }
                    default:
                        {
                            buffer = buffer + consoleKey.KeyChar;
                            break;
                        }
                }

            } while (consoleKey.Key != ConsoleKey.Escape);
        }

        public static string GetDefaultValues()
        {
            return "abc" + Environment.NewLine;
        }
        public static string ExecuteSalesForceDx(string inputString)
        {if(inputString == ">")
            {
                Console.WriteLine("force:alias           manage username aliases");
                Console.WriteLine("force:apex           work with Apex code");
                Console.WriteLine("force:auth           authorize an org for use with the Salesforce CLI");
                Console.WriteLine("force:config         configure the Salesforce CLI");
                Console.WriteLine("force:data           manipulate records in your org");
                Console.WriteLine("force:doc            display help for force commands");
                Console.WriteLine("force:lightning      create and test Lightning component bundles");
                Console.WriteLine("force:limits         view your org’s limits");
                Console.WriteLine("force:mdapi          retrieve and deploy metadata using Metadata API");
                Console.WriteLine("force:org            manage your Salesforce DX orgs");
                Console.WriteLine("force:package        install and uninstall first - and second - generation packages");
                Console.WriteLine("force:package1       develop first - generation managed and unmanaged packages");
                Console.WriteLine("force:package2       develop second - generation packages");
                Console.WriteLine("force:project        set up a Salesforce DX project");
                Console.WriteLine("force:schema         view standard and custom objects");
                Console.WriteLine("force:source         sync your project with your orgs");
                Console.WriteLine("force:user           perform user - related admin tasks");
                Console.WriteLine("force:visualforce    create and edit Visualforce files");
            }
            if (inputString.Length > 0)
            {

                var lastIndex = inputString.LastIndexOf('>');
                inputString = inputString.Substring(lastIndex);
                inputString = "force:" + inputString;
                return "Ran Code On : " + inputString;
            }
            return "Empty Command Sent";
        }

        public static void CommandLineAnalyzerReference(string command)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = @"C:\DevSharp\Sfdx\";
            p.StartInfo.FileName = @"C:\Program Files\sfdx\bin\sfdx.exe";
            p.StartInfo.Arguments = command;
            p.Start();
            p.WaitForExit();

            using (StreamReader reader = p.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);
            }
        }

        public static void ExampleFromNet()
        {
            // Configure console.
            Console.BufferWidth = 200;
            Console.WindowWidth = Console.BufferWidth;
            Console.TreatControlCAsInput = true;

            string inputString = String.Empty;
            ConsoleKeyInfo keyInfo;

            Console.WriteLine("Enter a string. Press <Enter> or Esc to exit.");
            do
            {
                keyInfo = Console.ReadKey(true);
                // Ignore if Alt or Ctrl is pressed.
                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                    continue;
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    continue;
                // Ignore if KeyChar value is \u0000.
                if (keyInfo.KeyChar == '\u0000') continue;
                // Ignore tab key.
                if (keyInfo.Key == ConsoleKey.Tab) continue;
                // Handle backspace.
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    // Are there any characters to erase?
                    if (inputString.Length >= 1)
                    {
                        // Determine where we are in the console buffer.
                        int cursorCol = Console.CursorLeft - 1;
                        int oldLength = inputString.Length;
                        int extraRows = oldLength / 80;

                        inputString = inputString.Substring(0, oldLength - 1);
                        Console.CursorLeft = 0;
                        Console.CursorTop = Console.CursorTop - extraRows;
                        Console.Write(inputString + new String(' ', oldLength - inputString.Length));
                        Console.CursorLeft = cursorCol;
                    }
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) break;
                // Handle key by adding it to input string.
                Console.Write(keyInfo.KeyChar);
                inputString += keyInfo.KeyChar;
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine("\n\nYou entered:\n    {0}",
                String.IsNullOrEmpty(inputString) ? "<nothing>" : inputString);

            Console.ReadLine();
        }
    }
}
