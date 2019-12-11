using Microsoft.Win32;
using System.IO;
using System.Windows;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Input;
using System.Text.RegularExpressions;
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
                if (FileFormat(fileName) == "txt")
                {
                    string text = FileReader.GetTxtString(fileName);
                    inputText.Text = text;
                    filePath.Text = fileName.ToString();
                }
                else if (FileFormat(fileName) == "docx")
                {
                    string text = FileReader.GetDocxString(fileName);
                    inputText.Text = text;
                    filePath.Text = fileName.ToString();
                }
                else
                {
                    MessageBox.Show("Неверный формат файла!");
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
                        using (WordprocessingDocument wordDocument =
                            WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
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
