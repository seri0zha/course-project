using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace CourseProject.Tests
{
    [TestClass()]
    public class VigenereTests
    {
        [TestMethod()]
        public void Encode_OnlyCyrillicLetters() // Проверка шифрования файла содержащего только буквы и пробельные символы
        {
            string input = File.ReadAllText(@"../../testFiles/Encoding/Test1.txt", Encoding.Default);   
            string expectedResult = File.ReadAllText(@"../../testFiles/Decoding/Test1.txt", Encoding.UTF8);
            input = Vigenere.Encode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Encode_CyrilicLettersAndNumbers() // Проверка зашифровки файла содержащего цифры, буквы и пробельные символы
        {
            string input = File.ReadAllText(@"../../testFiles/Encoding/Test2.txt", Encoding.Default);
            string expectedResult = File.ReadAllText(@"../../testFiles/Decoding/Test2.txt", Encoding.UTF8);
            input = Vigenere.Encode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Encode_OnlyLatinAlphabet() // Проверка зашифровки файла содержащего только латинский алфавит
        {
            string input = File.ReadAllText(@"../../testFiles/Encoding/Test3.txt", Encoding.Default);
            string expectedResult = File.ReadAllText(@"../../testFiles/Decoding/Test3.txt", Encoding.UTF8);
            input = Vigenere.Encode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Encode_OnlyNumbers() // Проверка зашифровки файла содержащего только цифры
        {
            string input = File.ReadAllText(@"../../testFiles/Encoding/Test4.txt", Encoding.Default);
            string expectedResult = File.ReadAllText(@"../../testFiles/Decoding/Test4.txt", Encoding.UTF8);
            input = Vigenere.Encode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Decode_OnlyCyrillicLetters() // Проверка шифрования файла содержащего только буквы и пробельные символы
        {
            string input = File.ReadAllText(@"../../testFiles/Decoding/Test1.txt", Encoding.UTF8);
            string expectedResult = File.ReadAllText(@"../../testFiles/Encoding/Test1.txt", Encoding.Default);
            input = Vigenere.Decode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Decode_CyrilicLettersAndNumbers() // Проверка зашифровки файла содержащего цифры, буквы и пробельные символы
        {
            string input = File.ReadAllText(@"../../testFiles/Decoding/Test2.txt", Encoding.UTF8);
            string expectedResult = File.ReadAllText(@"../../testFiles/Encoding/Test2.txt", Encoding.Default);
            input = Vigenere.Decode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Decode_OnlyLatinAlphabet() // Проверка зашифровки файла содержащего только латинский алфавит
        {
            string input = File.ReadAllText(@"../../testFiles/Decoding/Test3.txt", Encoding.UTF8);
            string expectedResult = File.ReadAllText(@"../../testFiles/Encoding/Test3.txt", Encoding.Default);
            input = Vigenere.Decode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }

        [TestMethod()]
        public void Decode_OnlyNumbers() // Проверка зашифровки файла содержащего только цифры
        {
            string input = File.ReadAllText(@"../../testFiles/Decoding/Test4.txt", Encoding.UTF8);
            string expectedResult = File.ReadAllText(@"../../testFiles/Encoding/Test4.txt", Encoding.Default);
            input = Vigenere.Decode(input, "скорпион");
            Assert.AreEqual(input, expectedResult);
        }
    }
}