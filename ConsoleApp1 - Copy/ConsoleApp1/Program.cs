using CSharpFundamentals.Apps.PasswordManager;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static List<string> _Str1DArray = new() {"A", "h", "m", "e", "d"};
        private static List<string> _IsOddEvenChar = new() {"1A", "0a"};
        private static List<int[]> _Int1DArray = new();
        private static List<bool> _EvenOdd = new();
        private static List<char> _EvenOddChar = new();
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

        private static void SaveAll()
        {
            var sb = new StringBuilder();
            foreach (var entry in _Str1DArray)
            {
                var Mat = EncryptionUtility.Encrypt(entry);
                for (int i = 0; i < Mat.Length; i++)
                {
                    if (i == (Mat.Length - 1))
                        sb.Append(Mat[i]);
                    else
                        sb.Append($"{Mat[i]}-");
                }
            }
            File.WriteAllText("Matrices.txt", sb.ToString());

            var sb1 = new StringBuilder();
            foreach (var item in _IsOddEvenChar)
            {
                sb1.Append($"{item[0]}-{item[1]}");
                sb1.AppendLine();
            }
            File.WriteAllText("OddEvenChar.txt", sb1.ToString());
        }

        private static void ReadAll()
        {
            if (File.Exists("Matrices.txt"))
            {
                var passwordLines = File.ReadAllText("Matrices.txt");
                foreach (var line in passwordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var sb = new StringBuilder();
                        foreach (var item in line)
                        {
                            string[] str = item.ToString().Split("-");
                            for (int i = 0; i < str.Length; i++)
                            {
                                sb.Append(str[i]);
                            }
                            string Mat = EncryptionUtility.Decrypt(sb.ToString());
                            char[] arrChar = Mat.ToCharArray();

                            int[] arrInt = new int[arrChar.Length];
                            for (int j = 0; j < arrInt.Length; j++)
                            {
                                arrInt[j] = (int)arrChar[j];
                            }
                            _Int1DArray.Add(arrInt);
                        }
                    }
                }
            }
            if (File.Exists("OddEvenChar.txt"))
            {
                var passwordLines = File.ReadAllText("OddEvenChar.txt");
                foreach (var line in passwordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('-');
                        var OddEvenStatus = line.Substring(0, equalIndex);
                        var EvenOddChar = line.Substring(equalIndex + 1).ToCharArray();
                        if (OddEvenStatus == "0")
                            _EvenOdd.Add(false);
                        else
                            _EvenOdd.Add(true);

                        _EvenOddChar.Add(EvenOddChar[0]);
                    }
                }
            }
        }

        static void Main(string[] args)
        {

            SaveAll();
            ReadAll();
            //var sb = new StringBuilder();
            //foreach (var entry in _1DArray)
            //{
            //    var Mat = EncryptionUtility.Encrypt(entry);
            //    for (int i = 0; i < Mat.Length; i++)
            //    {
            //        if (i == (Mat.Length - 1))
            //            sb.Append(Mat[i]);
            //        else
            //            sb.Append($"{Mat[i]}-");
            //    }
            //    sb.AppendLine();
            //}
            //Console.WriteLine(sb.ToString());
        }
    }
}
