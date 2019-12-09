using System;

namespace CourseProject
{
    public class Vigenere
    {
        private readonly static char[] alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
        private readonly static int N = alphabet.Length;
        public static string Encode(string inputString, string key)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char ch in inputString)
            {
                if ((char.ToLower(ch) >= 1072 && char.ToLower(ch) <= 1103) || char.ToLower(ch) == 1105)
                {
                    int c = (Array.IndexOf(alphabet, char.ToLower(ch)) + Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                    result += alphabet[c];
                    else
                        result += char.ToUpper(alphabet[c]);
                    keyIndex++;
                    if (keyIndex == key.Length)
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
                    int p = (Array.IndexOf(alphabet, char.ToLower(ch)) + N - Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                    result += alphabet[p];
                    else
                        result += char.ToUpper(alphabet[p]);
                    keyIndex++;
                    if (keyIndex == key.Length)
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
