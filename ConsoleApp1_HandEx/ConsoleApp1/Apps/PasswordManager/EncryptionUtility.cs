using System;
using System.Text;

namespace CSharpFundamentals.Apps.PasswordManager
{
    public class EncryptionUtility
    {
        private static readonly string _originalChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string _altChars =      @"gFCB4IiX1wDTEU5vlzu82GRLnNKoZ3kYAOcV6S0f9xebm7sryPtJMqdhQjaWpH";

        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return password;

            var sb = new StringBuilder();
            foreach (char ch in password)
            {
                var charIndex = _originalChars.IndexOf(ch);
                if (charIndex == -1)
                    throw new ArgumentException($"Invalid character '{ch}' in password. Only alphanumeric characters are allowed.");

                sb.Append(_altChars[charIndex]);
            }
            return sb.ToString();
        }

        public static string Decrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return password;

            var sb = new StringBuilder();
            foreach (char ch in password)
            {
                var charIndex = _altChars.IndexOf(ch);
                if (charIndex == -1)
                    throw new ArgumentException($"Invalid encrypted character '{ch}' found.");

                sb.Append(_originalChars[charIndex]);
            }
            return sb.ToString();
        }
    }
}