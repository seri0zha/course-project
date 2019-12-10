using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading;
using DocumentFormat.OpenXml;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, RoutedEventArgs e)
        {

            string fileName = GetFileName();
            if (!string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(fileName);
                var inputEncoding = Encoding.GetEncoding("iso-8859-1");
                if (FileFormat(fileName) == "txt")
                {
                    string text = File.ReadAllText(fileName, Encoding.UTF8);
                    if (IsValidISO(ConvertToUtf8(text)))
                    {
                        text = File.ReadAllText(fileName, Encoding.Default);
                    }
                    inputText.Text = text;
                    filePath.Text = fileName.ToString();
                }
                else if (FileFormat(fileName) == "docx")
                {
                    Stream stream = File.Open(fileName, FileMode.Open);
                    using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, false))
                    {
                        try
                        {
                            Body body = wordDocument.MainDocumentPart.Document.Body;
                            string content = body.InnerText;
                            inputText.Text = content;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка! {ex.Message}");
                        }
                    }
                }
            }
        }

        private string GetFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*| Text files (*.txt)|*.txt| Word documents (*.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        private string FileFormat(string fileName)
        {
            return fileName.Split('.')[fileName.Split('.').Length - 1];
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(keyInput.Text))
            { 
                outputText.Text = Vigenere.Decode(inputText.Text, keyInput.Text);
            }
        }
        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(keyInput.Text))
            {
                outputText.Text = Vigenere.Encode(inputText.Text, keyInput.Text);
            }
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

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(outputText.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt| Word documents (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == true)
                {

                    string fileName = saveFileDialog.FileName;
                    if (fileName.Split('.')[fileName.Split('.').Length - 1] == "txt")
                    {
                        File.WriteAllText(fileName, outputText.Text);
                    }
                    else
                    {
                        using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
                        {
                            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                            mainPart.Document = new Document(
                                new Body(
                                    new Paragraph(
                                        new Run(
                                            new Text(outputText.Text)))));
                        }
                    }
                }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) 
        {
            Regex regex = new Regex($"[{new string(Vigenere.alphabet)}{(new string(Vigenere.alphabet).ToUpper())}]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
