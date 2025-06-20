using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Apps.RandomStringNumberDictionary
{
    public class App
    {
        public static void GeneratedRandomNumber(int Min, int Max)
        {
            var rnd = new Random();
            int value = rnd.Next(Min, Max);

            Console.WriteLine($"Dandom value: {value}");
        }

        static void GenerateRandomString(int length)
        {
            var CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var SmallLetters = "abcdefghijklmnopqrstuvwxyz";
            var Numbers = "0123456789";
            var Symbols = "~!@#$%^&*()_{}|?";

            var charMap = new Dictionary<char, string>
            {
                { '1',  CapitalLetters },
                { '2',  SmallLetters },
                { '3',  Numbers },
                { '4',  Symbols },
            };


            Console.WriteLine("Choose What you want to include: ");
            Console.WriteLine("[1] CapitalLetters\t [2] SmallLetters");
            Console.WriteLine("[3] Numbers\t [4] Symbols");

            string Included = Console.ReadLine();
            if (Included == null)
                Included = "1234";

            var ConcatinatedString = new StringBuilder();

            foreach (char c in Included.Distinct())
            {
                if (charMap.TryGetValue(c, out var value))
                    ConcatinatedString.Append(value);
                else
                    throw new Exception($"Invalid option: {c}");
            }


            var sb = new StringBuilder();
            var rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                var randomIndex = rnd.Next(0, ConcatinatedString.Length);
                sb.Append(ConcatinatedString[randomIndex]);
            }

            Console.WriteLine($"Random String: {sb}");
        }

        public static void Run(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            while (true)
            {
                Console.WriteLine("please select an option: ");
                Console.WriteLine("[1] GeneratedRandomNumber\t [2] GenerateRandomString");
                string selectedOption = Console.ReadLine();
                if (selectedOption == "1")
                {
                    Console.WriteLine("Enter the Boundaries (Min, Max)");
                    int Min = int.Parse(Console.ReadLine());
                    int Max = int.Parse(Console.ReadLine());

                    GeneratedRandomNumber(Min, Max);
                }
                else if (selectedOption == "2")
                {
                    Console.WriteLine("Enter the Buffer Size");
                    GenerateRandomString(int.Parse(Console.ReadLine()));
                }
            }
        }
    }
}
