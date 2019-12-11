using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Tests
{
    [TestClass()]
    public class FileReaderTests
    {
        [TestMethod()]
        public void GetTxtString_Test1()
        {
            string fileContent = FileReader.GetTxtString(@"../../testFiles/ReadingTest1.txt");
            string expectedResult = "Я помню чудное мгновенье: Передо мной явилась ты, Как мимолетное виденье, Как гений чистой красоты.";
            Assert.AreEqual(fileContent, expectedResult);
        }

        [TestMethod()]
        public void GetTxtString_ReadEmptyFile_Test2()
        {
            string fileContent = FileReader.GetTxtString(@"../../testFiles/ReadingTest2.txt");
            string expectedResult = "";
            Assert.AreEqual(fileContent, expectedResult);
        }
        [TestMethod()]
        public void GetDocxString_Test3()
        {
            string fileContent = FileReader.GetDocxString(@"../../testFiles/ReadingTest3.docx");
            string expectedResult = "Еще больше бессмысленного текста Делаем Больше Новых Линий !!!!!!!";
            Assert.AreEqual(fileContent, expectedResult);
        }

        [TestMethod()]
        public void GetDocxStringTest_ReadEmptyFile_Test4()
        {
            string fileContent = FileReader.GetDocxString(@"../../testFiles/ReadingTest4.docx");
            string expectedResult = "";
            Assert.AreEqual(fileContent, expectedResult);
        }
    }
}