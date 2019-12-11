using System;

namespace CourseProject
{
    public class Vigenere
    {
        public readonly static char[] alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
        private readonly static int N = alphabet.Length;

        /*
         * Если N   количество букв в алфавите, m — буквы открытого текста, 
         * k - буквы ключа, то код шифрованного методом Виженера символа 
         * можно записать следующим образом:
         * c = (m + k) mod N
         * Код расшифрованного символа считается по следующей формуле:
         * m = (c + N - k) mod N
         */
        public static string Encode(string inputString, string key)
        {
            string result = "";
            int keyIndex = 0;
            foreach (char ch in inputString)
            {
                // проверка, является ли символ исходной строки буквой алфавита
                if ((char.ToLower(ch) >= 1072 && char.ToLower(ch) <= 1103) || char.ToLower(ch) == 1105)
                {
                    int c = (Array.IndexOf(alphabet, char.ToLower(ch)) + 
                        Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                        result += alphabet[c];
                    else
                        result += char.ToUpper(alphabet[c]);
                    keyIndex++;
                    if (keyIndex == key.Length) // если индекс символ ключа является последним, то идем по ключу заново
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
                    int m = (Array.IndexOf(alphabet, char.ToLower(ch)) + 
                        N - Array.IndexOf(alphabet, char.ToLower(key[keyIndex]))) % N;
                    if (char.IsLower(ch))
                    result += alphabet[m];
                    else
                        result += char.ToUpper(alphabet[m]);
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
