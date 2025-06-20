using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpFundamentals.Apps.CommandLine
{
    public class App
    {
        static void MakeDirector(string path)
        {
            Directory.CreateDirectory(path);
            Console.WriteLine($"Directory created: {path}");
        }
        static void RemoveDirector(string path)
        {
            // if (Directory.Exists(path))
            // Directory.Delete(path, true); // delete dir and its subfolders
            if (Directory.Exists(path))
            {
                Directory.Delete(path); // delete an empty directory
                Console.WriteLine($"Directory removed: {path}");
            }
            else
            {
                Console.WriteLine("Directory was not found");
            }
        }
        static void RemoveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"Directory removed: {path}");
            }
            else
            {
                Console.WriteLine("Directory was not found");
            }
        }
        static void List_Documents(string path)
        {
            foreach (var Dir in Directory.GetDirectories(path))
                Console.WriteLine($"[Dir]: {Dir}");
            foreach (var File in Directory.GetFiles(path))
                Console.WriteLine($"[File]: {File}");
        }
        static void DisplayInfo(string path)
        {
            if (Directory.Exists(path))
            {
                var DirInfo = new DirectoryInfo(path);
                Console.WriteLine("Type: Directory");
                Console.WriteLine($"Creation Time: {DirInfo.CreationTime}");
                Console.WriteLine($"Last Modified At: {DirInfo.LastWriteTimeUtc}");
            }
            else if (File.Exists(path))
            {
                var FileInfo = new FileInfo(path);
                Console.WriteLine("Type: File");
                Console.WriteLine($"Creation Time: {FileInfo.CreationTime}");
                Console.WriteLine($"Last Modified At: {FileInfo.LastWriteTimeUtc}");
                Console.WriteLine($"File Size: {FileInfo.Length}");
            }
        }
        static void Help(HashSet<string> validcommands)
        {
            Console.Write("Commands: ");
            foreach (var cmd in validcommands)
            {
                Console.Write($"{cmd}, ");
            }
            Console.WriteLine();
        }
        static void GoTo(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.SetCurrentDirectory(path);
            }
            else
            {
                Console.WriteLine("The system cannot find the path specified.");
            }
        }
        static void Print(string path)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                Console.WriteLine(content);
            }
        }
        static void Write(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Enter Text: ");
                var content = Console.ReadLine();
                File.WriteAllText(path, content);
                Console.WriteLine($"File Modified: {path}");
            }
        }
        static void Append(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Enter Text: ");
                var content = Console.ReadLine();
                File.AppendAllText(path, content);
                Console.WriteLine($"File Modified: {path}");
            }
        }
        static void MakeFile(string path)
        {
            File.Create(path);
            Console.WriteLine($"Directory created: {path}");
        }
        public static void Run(string[] args)
        {
            var validcommands = new HashSet<string>
            {
                "list", "info", "mkdir", "md", "print", "write", "append", "rmdir", "exit", "create", "cd", "help", "clear","del", "erase"
            };

            var DriveCtrl = new HashSet<string>
            {
                "d:", "c:"
            };
            Console.WriteLine();
            Directory.SetCurrentDirectory(@"C:\Users\Ahmood");
            Console.Write(Directory.GetCurrentDirectory() + "> ");

            while (true)
            {
                var input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                int WhiteSpaceIndex = input.IndexOf(' ');

                string path, command;

                if (WhiteSpaceIndex == -1)
                {
                    command = input.ToLower();
                    path = ""; // or null, depending on how you want to handle it
                }
                else
                {
                    command = input.Substring(0, WhiteSpaceIndex).ToLower();
                    path = input.Substring(WhiteSpaceIndex + 1).Trim();
                }

                if (Regex.IsMatch(command, @"^[A-Z]:$", RegexOptions.IgnoreCase))
                {
                    string driveLetter = command.ToUpper()[0].ToString();
                    string drivePath = driveLetter + @":\";

                    if (Directory.Exists(drivePath))
                    {
                        Directory.SetCurrentDirectory(drivePath);
                    }
                    else
                    {
                        Console.WriteLine($"Drive {driveLetter}: does not exist or is not ready.");
                    }
                }

                if (!DriveCtrl.Contains(command))
                {
                    if (!validcommands.Contains(command))
                    {
                        Console.WriteLine("Invalid command. Type 'help' if you need a list of available commands.");
                        continue;
                    }
                }

                if (command == "cd")
                {
                    GoTo(path);
                }
                else if (command == "list")
                {
                    List_Documents(path);
                }
                else if (command == "help")
                {
                    Help(validcommands);
                }
                else if (command == "info")
                {
                    DisplayInfo(path);
                }
                else if (command == "mkdir" || command == "md")
                {
                    MakeDirector(path);
                }
                else if (command == "print")
                {
                    Print(path);
                }
                else if (command == "write")
                {
                    Write(path);
                }
                else if (command == "clear")
                {
                    Console.Clear();
                }
                else if (command == "append")
                {
                    Append(path);
                }
                else if (command == "create")
                {
                    MakeFile(path);
                }
                else if (command == "rmdir")
                {
                    RemoveDirector(path);
                }
                else if (command == "del" || command == "erase")
                {
                    RemoveFile(path);
                }
                else if (command == "exit")
                {
                    break;
                }

                Console.WriteLine();
                Console.Write(Directory.GetCurrentDirectory() + "> ");
            }

        }
    }
}
