using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Apps.PasswordManager
{
    // main methods
    // 1. list all 
    // 2. add\change 
    // 3. remove 
    // 4. GetPassword

    // support Methods
    // saveAllPasswords
    // readAllPasswords
    

    public class App
    {
        private static readonly Dictionary<string, string> _passwordEntries = new();

        public static void Run(string[] arges)
        {
            bool IsAllowed = readAllPasswords();
            if (!IsAllowed)
                return;
            while (true)
            {
                Console.WriteLine("1. List all passwords: ");
                Console.WriteLine("2. Add/Change a password: ");
                Console.WriteLine("3. Delete a password: ");
                Console.WriteLine("4. Get a password: ");
                Console.WriteLine("5. Exit ");
                var input = Console.ReadLine();
                if (input == "1")
                    ListAllPasswords();
                else if (input == "2")
                    AddOrChangePassword();
                else if (input == "3")
                    RemovePassword();
                else if (input == "4")
                    GetPassword();
                else if (input == "5")
                    break;
                else
                        Console.WriteLine("Error, Invalid Choice: ");

                Console.WriteLine("------------------------");
            }
        }

        private static void ListAllPasswords()
        {
            foreach (var entry in _passwordEntries)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }

        private static void AddOrChangePassword()
        {
            Console.Write("Enter app name: ");
            var key = Console.ReadLine();
            Console.Write("Enter app password: ");
            // getIndex of an element in a dictionary
            var Index = Array.IndexOf(_passwordEntries.Keys.ToArray(), key);
            var newPassword = Console.ReadLine();
            if (_passwordEntries.ContainsKey(key))
                _passwordEntries[key] = newPassword;
                
            else 
                _passwordEntries.Add(key, newPassword);


            saveAllPasswords();
        }

        private static void RemovePassword()
        {
            Console.Write("Enter app name: ");
            var key = Console.ReadLine();
            if (_passwordEntries.ContainsKey(key))
            {
                _passwordEntries.Remove(key);
                saveAllPasswords();
            }
            else 
                Console.WriteLine("Password not found");
        }

        private static void GetPassword()
        {
            Console.Write("Enter app name: ");
            var appName = Console.ReadLine();
            if (_passwordEntries.ContainsKey(appName))
                Console.WriteLine($"{_passwordEntries[appName]}");
            else
                Console.WriteLine("Password not found");
        }

        private static void saveAllPasswords()
        {
            var sb = new StringBuilder();
            foreach (var entry in _passwordEntries)
                sb.AppendLine($"{entry.Key}={EncryptionUtility.Encrypt(entry.Value)}");
            File.WriteAllText("password.txt", sb.ToString());
        }

        private static bool readAllPasswords()
        {
            if (File.Exists("password.txt"))
            {
                var passwordLines = File.ReadAllText("password.txt");
                foreach (var line in passwordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('=');
                        var appName = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);
                        _passwordEntries.Add(appName, EncryptionUtility.Decrypt(password));
                    }
                }
            }
            var mKey = "MasterKey";
            if (!_passwordEntries.ContainsKey(mKey))
            {
                Console.Write("WelCome Please Set The Master Password: ");
                var mPassword = Console.ReadLine();
                ArgumentException.ThrowIfNullOrEmpty(mPassword);
                _passwordEntries.Add(mKey, mPassword);
                saveAllPasswords();
                return true;
            }
            else
            {
                Console.Write("Enter mPassword: ");
                var mPassword = Console.ReadLine();
                if (_passwordEntries[mKey].Equals(mPassword))
                {
                    Console.WriteLine("WelCome To Your Program");
                    return true;
                }
                else
                {
                    Console.WriteLine("GO Away Hacker --__--");
                    return false;
                }
            }
        }
    }
}
