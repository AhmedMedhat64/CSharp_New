using CSharpFundamentals.Apps.PasswordManager;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly Dictionary<string, string> dict = new() {};
        public static string Shuffle(string password)
        {
            char[] array = password.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        static void Main(string[] args)
        {
            App.Run(args);
        }
    }
}
