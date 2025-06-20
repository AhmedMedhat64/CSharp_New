using CSharpFundamentals.Apps.RandomStringNumberDictionary;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CSharpFundamentals.Apps.PasswordManager
{

    public class EncryptionAndDecryption
    {
        // * I want to try to methods in solution
        // * first:  using numbers from 0 to EncryptingAndDecryptingStr.Length - 1
        // * second: using ASCII Value of letters in EncryptingAndDecryptingStr

        // Encrypting matrix 2d matrix
        // calc decrypting matix (Method) return type 2d matrix
        // string with all capiitial letters and small and nums from 0 to 9
        // check the length of the password 
        // what to do if length is odd in encryption
        // what to do if length is odd in decryption

        // lets start 
        private static Dictionary<int, char> EncryptingTable = new() {
            { 0 , 'A' },
            { 1 , 'B' },
            { 2 , 'C' },
            { 3 , 'D' },
            { 4 , 'E' },
            { 5 , 'F' },
            { 6 , 'G' },
            { 7 , 'H' },
            { 8 , 'I' },
            { 9 , 'J' },
            { 10 , 'K' },
            { 11 , 'L' },
            { 12 , 'M' },
            { 13 , 'N' },
            { 14 , 'O' },
            { 15 , 'P' },
            { 16 , 'Q' },
            { 17 , 'R' },
            { 18 , 'S' },
            { 19 , 'T' },
            { 20 , 'U' },
            { 21 , 'V' },
            { 22 , 'W' },
            { 23 , 'X' },
            { 24 , 'Y' },
            { 25 , 'Z' },
            { 26 , 'a' },
            { 27 , 'b' },
            { 28 , 'c' },
            { 29 , 'd' },
            { 30 , 'e' },
            { 31 , 'f' },
            { 32 , 'g' },
            { 33 , 'h' },
            { 34 , 'i' },
            { 35 , 'j' },
            { 36 , 'k' },
            { 37 , 'l' },
            { 38 , 'm' },
            { 39 , 'n' },
            { 40 , 'o' },
            { 41 , 'p' },
            { 42 , 'q' },
            { 43 , 'r' },
            { 44 , 's' },
            { 45 , 't' },
            { 46 , 'u' },
            { 47 , 'v' },
            { 48 , 'w' },
            { 49 , 'x' },
            { 50 , 'y' },
            { 51 , 'z' },
            { 52 , '0' },
            { 53 , '1' },
            { 54 , '2' },
            { 55 , '3' },
            { 56 , '4' },
            { 57 , '5' },
            { 58 , '6' },
            { 59 , '7' },
            { 60 , '8' },
            { 61 , '9' }
        };
        private static Dictionary<char, int> DecryptingTable = new() {
            { 'A' , 0 },
            { 'B' , 1 },
            { 'C' , 2 },
            { 'D' , 3 },
            { 'E' , 4 },
            { 'F' , 5 },
            { 'G' , 6 },
            { 'H' , 7 },
            { 'I' , 8 },
            { 'J' , 9 },
            { 'K' , 10 },
            { 'L' , 11 },
            { 'M' , 12 },
            { 'N' , 13 },
            { 'O' , 14 },
            { 'P' , 15 },
            { 'Q' , 16 },
            { 'R' , 17 },
            { 'S' , 18 },
            { 'T' , 19 },
            { 'U' , 20 },
            { 'V' , 21 },
            { 'W' , 22 },
            { 'X' , 23 },
            { 'Y' , 24 },
            { 'Z' , 25 },
            { 'a' , 26 },
            { 'b' , 27 },
            { 'c' , 28 },
            { 'd' , 29 },
            { 'e' , 30 },
            { 'f' , 31 },
            { 'g' , 32 },
            { 'h' , 33 },
            { 'i' , 34 },
            { 'j' , 35 },
            { 'k' , 36 },
            { 'l' , 37 },
            { 'm' , 38 },
            { 'n' , 39 },
            { 'o' , 40 },
            { 'p' , 41 },
            { 'q' , 42 },
            { 'r' , 43 },
            { 's' , 44 },
            { 't' , 45 },
            { 'u' , 46 },
            { 'v' , 47 },
            { 'w' , 48 },
            { 'x' , 49 },
            { 'y' , 50 },
            { 'z' , 51 },
            { '0' , 52 },
            { '1' , 53 },
            { '2' , 54 },
            { '3' , 55 },
            { '4' , 56 },
            { '5' , 57 },
            { '6' , 58 },
            { '7' , 59 },
            { '8' , 60 },
            { '9' , 61 }
        };
        // save
        private static List<string> _Str1DArray = new();
        // read
        private static List<int[]> _Int1DArray = new();
        // save
        private static List<string> _IsOddEvenChar = new();
        // read
        private static List<bool> _EvenOdd = new();
        private static List<string> _EvenOddChar = new();
        private static List<int> _EncDet = new();

        static int[] modArray = {
            1, 3, 5, 7, 11, 13, 17, 19,
            21, 23, 25, 27, 31, 33, 35, 37,
            39, 41, 43, 45, 47, 49, 53, 55,
            57, 59, 61
        };

        // The array with their corresponding modular inverses (mod 62)
        static int[] modInverseArray = {
            1, 21, 25, 9, 17, 43, 11, 49,
            3, 27, 5, 23, 2, 47, 53, 57,
            47, 45, 13, 55, 39, 19, 35, 45,
            37, 59, 61
        };

        public static void rvPassSave(int rvIndex)
        {
            try
            {
                // Check if index is valid for all lists
                if (rvIndex >= 0 &&
                    rvIndex < _Int1DArray.Count &&
                    rvIndex < _EvenOdd.Count &&
                    rvIndex < _EvenOddChar.Count)
                {
                    _Int1DArray.RemoveAt(rvIndex);
                    _EvenOdd.RemoveAt(rvIndex);
                    _EvenOddChar.RemoveAt(rvIndex);

                    // Save the updated data
                    SaveMat();
                    SaveOddEvenChar();
                }
                else
                {
                    Console.WriteLine("Warning: Could not sync encryption data - index out of range");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in rvPassSave: {ex.Message}");
            }
        }

        public static void UpdateEncryptionData(int index, string encryptedPassword)
        {
            try
            {
                // Convert encrypted password to integer array
                int[] arrInt = new int[encryptedPassword.Length];
                for (int i = 0; i < encryptedPassword.Length; i++)
                {
                    arrInt[i] = encryptedPassword[i];
                }

                // Update or add the encryption data
                if (index >= 0 && index < _Int1DArray.Count)
                {
                    // Update existing entry
                    _Int1DArray[index] = arrInt;
                    _EvenOdd[index] = encryptedPassword.Length % 2 == 1;
                    _EvenOddChar[index] = encryptedPassword.Length > 0 ? encryptedPassword[0].ToString() : ' '.ToString();
                }
                else
                {
                    // Add new entry
                    _Int1DArray.Add(arrInt);
                    _EvenOdd.Add(encryptedPassword.Length % 2 == 1);
                    _EvenOddChar.Add(encryptedPassword.Length > 0 ? encryptedPassword[0].ToString() : ' '.ToString());
                }

                // Save the updated data
                SaveMat();
                SaveOddEvenChar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Encryption data not updated - {ex.Message}");
            }
        }

        public static void SaveMat()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var entry in _Str1DArray)
                {
                    var encrypted = EncryptionUtility.Encrypt(entry);
                    sb.AppendLine(string.Join("-", encrypted.ToCharArray()));
                }
                File.WriteAllText("Matrices.txt", sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving matrices: {ex.Message}");
            }
        }

        public static void ReadMat()
        {
            try
            {
                if (!File.Exists("Matrices.txt")) return;

                var lines = File.ReadAllLines("Matrices.txt");
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var encrypted = line.Replace("-", "");
                    var decrypted = EncryptionUtility.Decrypt(encrypted);

                    var arrInt = new int[decrypted.Length];
                    for (int i = 0; i < decrypted.Length; i++)
                    {
                        arrInt[i] = decrypted[i];
                    }
                    _Int1DArray.Add(arrInt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading matrices: {ex.Message}");
            }
        }

        public static void SaveOddEvenChar()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var item in _IsOddEvenChar)
                {
                    sb.AppendLine(string.Join("-", item.ToCharArray()));
                }
                File.WriteAllText("OddEvenChar.txt", sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving odd/even chars: {ex.Message}");
            }
        }

        public static void ReadOddEvenChar()
        {
            try
            {
                if (!File.Exists("OddEvenChar.txt")) return;

                var lines = File.ReadAllLines("OddEvenChar.txt");
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('-');
                    if (parts.Length < 2) continue;

                    if (parts[0] == "0")
                        _EvenOdd.Add(false);
                    else if (parts[0] == "1")
                        _EvenOdd.Add(true);

                    if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
                        _EvenOddChar.Add(parts[1][0].ToString());

                    if (parts.Length > 2 && int.TryParse(parts[2], out int det))
                        _EncDet.Add(det);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading odd/even chars: {ex.Message}");
            }
        }

        public static int[] _Calc1DArr()
        {
            int[] arr = new int[4];
            for (int i = 0; i < arr.Length; i++)
            {
                var rnd = new Random();
                int value = rnd.Next(1, 200);
                arr[i] = value % 62;
            }
            return arr;
        }


        public static string Encrypt(string password, int cnt)
        {
            bool IsValid = false;
            while (!IsValid)
            {
                int[] arrInt = _Calc1DArr();
                string EncDecChar = "A";
                int[,] EncMat = new int[2, 2];
                int count = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        EncMat[i, j] = arrInt[count++];
                    }
                } 
                int EncMatdet;
                EncMatdet = ((EncMat[0, 0] * EncMat[1, 1]) - (EncMat[0, 1] * EncMat[1, 0])) % 62;
                if (!(EncMatdet % 2 == 0 || EncMatdet % 31 == 0 || EncMatdet == 0))
                    continue;
                else
                {
                    IsValid = true;
                }
                    var sb = new StringBuilder();
                for (int i = 0; i < arrInt.Length; i++)
                {
                    sb.Append((char)arrInt[i]);
                }
                _Str1DArray.Add(sb.ToString());
                SaveMat();

                bool isOdd = (password.Length % 2 == 1);
                if (isOdd)
                {
                    password += EncDecChar;
                }

                // Encryption
                var EncPass = new StringBuilder();
                int iterations = password.Length / 2;

                count = 0;
                int FirstEle = 0;
                int LastEle = 0;
                int EleSum = 0;
                for (int i = 0; i < iterations; i++)
                {
                    int[,] arr = new int[2, 1];
                    arr[0, 0] = DecryptingTable[password[count]];
                    arr[1, 0] = DecryptingTable[password[count++]];
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            EleSum += (arr[FirstEle++, LastEle] * EncMat[k, j]);
                        }
                        EleSum = EleSum % 62;
                        EncPass.Append(EncryptingTable[EleSum]);
                        FirstEle = EleSum = 0;
                    }
                }


                if (isOdd)
                {
                    EncDecChar = EncPass.Length.ToString();
                    _IsOddEvenChar.Add($"1{EncDecChar}{EncMatdet}");
                    SaveOddEvenChar();
                    password = EncPass.ToString().Substring(0, EncPass.Length - 1);
                }
                else
                {
                    _IsOddEvenChar.Add($"0a{EncMatdet}");
                    SaveOddEvenChar();
                    password = EncPass.ToString();
                }
            }
            return password;
        }

        public static string Decrypt(string password, int cnt)
        {
            ReadMat();
            ReadOddEvenChar();
            int[,] arrDec = new int[2, 2];
            int Det = _EncDet[cnt];
            int[] arr = _Int1DArray[cnt];
            bool IsOdd = _EvenOdd[cnt];
            string IsOddChar = _EvenOddChar[cnt];
            int count = 3;
            for (int i = 0; i < modArray.Length; i++)
            {
                if (Det == modArray[i])
                {
                    Det = modInverseArray[i];
                    break;
                }
            }
            for (int j = 1; j >= 0; j--)
            {
                for (int k = 1; k >= 0; k--)
                {
                    if (j != k)
                        arrDec[j, k] = -arr[count--] * Det;
                    else 
                        arrDec[j, k] =  arr[count--] * Det;
                }
            }

            if (IsOdd)
            {
                password += IsOddChar;
            }

            
            var DecPass = new StringBuilder();
            int iterations = password.Length / 2;

            count = 0;
            int FirstEle = 0;
            int LastEle = 0;
            int EleSum = 0;
            for (int i = 0; i < iterations; i++)
            {
                int[,] arr1 = new int[2, 1];
                arr1[0, 0] = DecryptingTable[password[count]];
                arr1[1, 0] = DecryptingTable[password[count++]];
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        EleSum += (arr1[FirstEle++, LastEle] * arrDec[k, j]);
                    }
                    EleSum = EleSum % 62;
                    DecPass.Append(EncryptingTable[EleSum]);
                    FirstEle = EleSum = 0;
                }
            }
            if (IsOdd)
                password = DecPass.ToString().Substring(0, DecPass.Length - 1);
            else
                password = DecPass.ToString();

            return password;
        }
    }
}

