using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace CourseProject
{
    public class FileReader
    {
        
        public static string GetTxtString(string fileName)
        {
            string result = File.ReadAllText(fileName, Encoding.UTF8);
            if (IsValidISO(ConvertToUtf8(result)))
            {
                result = File.ReadAllText(fileName, Encoding.Default);
            }

            return result;
        }

        public static string GetDocxString(string fileName)
        {
            /*  
             *  Метод получения текста из word документа формата .docx
             *  Для работы с .docx использовалась библиотека OpenXML.
             */
            Stream stream = File.Open(fileName, FileMode.Open);
            string result = "";
            if (stream.Length != 0)
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, false))
                {
                    try
                    {
                        Body body = wordDocument.MainDocumentPart.Document.Body;
                        result = body.InnerText;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error ocurred during reading file! Message: {ex.Message}");
                    }
                }
            }
            return result;
        }

        private static bool IsValidISO(string input)
        {
            byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
            string result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
            return string.Equals(input, result);
        } // Result_v5.txt file was in iso-8859-1 charset, so I decided to check input file for this.

        private static string ConvertToUtf8(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        } // Converts input string to UTF8 charset
    }
}
