using System;
using System.Text;

namespace WinUI3DevTools.Classes
{
    /// <summary>
    /// Custom string functions class.
    /// </summary>
    internal class StringFunctions
    {
        /// <summary>
        /// Capitalizes the first letter of each words in a given the string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        internal static string CapitalizeString(string str)
        {
            str = str.ToLower();
            char[] strArray = str.ToCharArray();
            if (char.IsLower(strArray[0]))
            {
                strArray[0] = char.ToUpper(strArray[0]);
            }
            for (int i = 1; i < strArray.Length; i++)
            {
                if (!char.IsWhiteSpace(strArray[i - 1]))
                {
                    continue;
                }
                if (char.IsUpper(strArray[i]))
                {
                    continue;
                }
                strArray[i] = char.ToUpper(strArray[i]);
            }
            return new string(strArray);
        }

        /// <summary>
        /// Counts the lines in a given string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>An int.</returns>
        internal static int CountLines(string str)
        {
            return str.Length - str.ReplaceLineEndings("").Length + 1;
        }

        /// <summary>
        /// Counts the words in a given string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>An int.</returns>
        internal static int CountWords(string str)
        {
            int count = 0;
            char[] strArray = str.ToCharArray();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (char.IsWhiteSpace(strArray[i]))
                {
                    continue;
                }
                if (i + 1 == strArray.Length)
                {
                    count++;
                    continue;
                }
                if (char.IsWhiteSpace(strArray[i + 1]))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Decodes to normal string from base64 string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        internal static string DecodeBase64(string str)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(str));
        }

        /// <summary>
        /// Encodes from normal string to base64 string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        internal static string EncodeBase64(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }
    }
}