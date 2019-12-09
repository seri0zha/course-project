using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject
{
    class Vigenere
    {
        private readonly static char[] alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
        private readonly static int N = alphabet.Length;
        public static string Encode(string inputString, string key)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char ch in inputString)
            {
                if (char.ToLower(ch) >= 1072 && char.ToLower(ch) <= 1103 || char.ToLower(ch) == 1105)
                {
                    int c = (Array.IndexOf(alphabet, ch) + Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                        result += alphabet[c];
                    else
                        result += char.ToUpper(alphabet[c]);
                    if (keyIndex + 1 < key.Length)
                        keyIndex += 1;
                    else
                        keyIndex = 0;
                }
                else
                {
                    result += ch;
                }
            }
            return result;
        }

        public static string Decode(string inputString, string key)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char ch in inputString)
            {
                if (char.ToLower(ch) >= 1072 && char.ToLower(ch) <= 1103 || char.ToLower(ch) == 1105)
                {
                    int p = (Array.IndexOf(alphabet, ch) + N - Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                        result += alphabet[p];
                    else
                        result += char.ToUpper(alphabet[p]);
                    if (keyIndex + 1 < key.Length)
                        keyIndex += 1;
                    else
                        keyIndex = 0;
                }
                else
                {
                    result += ch;
                }
            }
            return result;
        }


    }
}
