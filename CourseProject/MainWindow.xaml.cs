using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

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
            inputText.Visibility = Visibility.Hidden;
            outputText.Visibility = Visibility.Hidden;
        }

        private void selectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                var inputEncoding = Encoding.GetEncoding("iso-8859-1");
                string text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                if (IsValidISO(ConvertToUtf8(text)))
                {
                    text = File.ReadAllText(openFileDialog.FileName, Encoding.Default);
                }
                inputText.Visibility = Visibility.Visible;
                inputText.Text = text;
                filePath.Text = openFileDialog.FileName.ToString();
            }
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(keyInput.Text))
            { 
                outputText.Text = Vigenere.Decode(inputText.Text, keyInput.Text);
                outputText.Visibility = Visibility.Visible;
            }
        }
        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(keyInput.Text))
            {
                outputText.Text = Vigenere.Encode(inputText.Text, keyInput.Text);
                outputText.Visibility = Visibility.Visible;
            }
        }
        private static bool IsValidISO(string input)
        {
            byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(input);
            string result = Encoding.GetEncoding("ISO-8859-1").GetString(bytes);
            return string.Equals(input, result);
        } // Result_v5.txt file was in iso-8859-1 charset, so I decided to check input file

        private static string ConvertToUtf8(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        } // Converts input string to UTF8 charset

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, outputText.Text);

        }
    }
}
