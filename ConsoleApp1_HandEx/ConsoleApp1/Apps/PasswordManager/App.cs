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
            try
            {
                Console.Write("Enter app name: ");
                var key = Console.ReadLine();
                Console.Write("Enter app password: ");
                var newPassword = Console.ReadLine();

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(newPassword))
                {
                    Console.WriteLine("Error: App name and password cannot be empty");
                    return;
                }

                bool isExisting = _passwordEntries.ContainsKey(key);

                if (isExisting)
                {
                    // For existing entries, update both password and encryption data
                    _passwordEntries[key] = newPassword;

                    // Get the index of the existing password
                    int indexToUpdate = _passwordEntries.Keys.ToList().IndexOf(key);
                    if (indexToUpdate >= 0 && key != "MasterKey")
                    {
                        // Regenerate encryption data for the updated password
                        string encrypted = EncryptionUtility.Encrypt(newPassword);
                        // Update the corresponding encryption data entry
                        EncryptionAndDecryption.UpdateEncryptionData(indexToUpdate, encrypted);
                    }
                }
                else
                {
                    // For new entries, just add to dictionary
                    _passwordEntries.Add(key, newPassword);
                }

                saveAllPasswords();
                Console.WriteLine($"Password for {key} {(isExisting ? "updated" : "added")} successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RemovePassword()
        {
            try
            {
                Console.Write("Enter app name: ");
                var key = Console.ReadLine();

                if (_passwordEntries.ContainsKey(key))
                {
                    // Remove from password dictionary
                    _passwordEntries.Remove(key);

                    // If using encryption data, remove the corresponding entry
                    if (key != "MasterKey") // Don't remove encryption data for master key
                    {
                        // Get the index of the password to remove
                        int indexToRemove = _passwordEntries.Keys.ToList().IndexOf(key);
                        if (indexToRemove >= 0)
                        {
                            EncryptionAndDecryption.rvPassSave(indexToRemove);
                        }
                    }

                    saveAllPasswords();
                    Console.WriteLine($"Password for {key} removed successfully.");
                }
                else
                {
                    Console.WriteLine("Password not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing password: {ex.Message}");
            }
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
            try
            {
                var sb = new StringBuilder();
                foreach (var entry in _passwordEntries)
                {
                    // Skip encrypting the master key
                    if (entry.Key == "MasterKey")
                    {
                        sb.AppendLine($"{entry.Key}={entry.Value}");
                        continue;
                    }

                    sb.AppendLine($"{entry.Key}={EncryptionUtility.Encrypt(entry.Value)}");
                }
                File.WriteAllText("password.txt", sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving passwords: {ex.Message}");
            }
        }

        private static bool readAllPasswords()
        {
            try
            {
                if (File.Exists("password.txt"))
                {
                    var lines = File.ReadAllLines("password.txt");
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var parts = line.Split('=');
                        if (parts.Length != 2) continue;

                        var appName = parts[0];
                        var password = parts[1];

                        // Skip decrypting the master key
                        if (appName == "MasterKey")
                        {
                            _passwordEntries[appName] = password;
                            continue;
                        }

                        _passwordEntries[appName] = EncryptionUtility.Decrypt(password);
                    }
                }

                // Master key setup/validation
                const string mKey = "MasterKey";
                if (!_passwordEntries.ContainsKey(mKey))
                {
                    Console.Write("Welcome! Please set the master password: ");
                    var mPassword = Console.ReadLine();
                    ArgumentException.ThrowIfNullOrEmpty(mPassword);
                    _passwordEntries.Add(mKey, mPassword);
                    saveAllPasswords();
                    return true;
                }

                Console.Write("Enter master password: ");
                var inputPassword = Console.ReadLine();
                if (_passwordEntries[mKey].Equals(inputPassword))
                {
                    Console.WriteLine("Welcome to your program!");
                    return true;
                }

                Console.WriteLine("Access denied.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading passwords: {ex.Message}");
                return false;
            }
        }
    }
}
