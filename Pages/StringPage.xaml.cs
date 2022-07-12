using Microsoft.UI.Xaml.Controls;
using System;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using WinUI3DevTools.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StringPage : Page
    {
        public ObservableBoolean TextFunctionsIsEnabled = new() { Value = false };

        /// <summary>
        /// Initializes a new instance of the <see cref="StringPage"/> class.
        /// </summary>
        public StringPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles command buttons click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            switch ((sender as AppBarButton).Tag.ToString())
            {
                case "Clear":
                    InputTextBox.Text = string.Empty;
                    OutputTextBox.Text = string.Empty;
                    break;

                case "Copy":
                    DataPackage dataPackage =
                        new() { RequestedOperation = DataPackageOperation.Copy };
                    dataPackage.SetText(OutputTextBox.Text);
                    Clipboard.SetContent(dataPackage);
                    break;

                case "Encode64":
                    OutputTextBox.Text = Convert.ToBase64String(
                        Encoding.Default.GetBytes(InputTextBox.Text)
                    );
                    break;

                case "Decode64":
                    try
                    {
                        OutputTextBox.Text = Encoding.Default.GetString(
                            Convert.FromBase64String(InputTextBox.Text)
                        );
                    }
                    catch (FormatException) { }
                    break;

                case "Uppercase":
                    OutputTextBox.Text = InputTextBox.Text.ToUpper();
                    break;

                case "Lowercase":
                    OutputTextBox.Text = InputTextBox.Text.ToLower();
                    break;

                case "Capitalize":
                    OutputTextBox.Text = CapitalizeString(InputTextBox.Text);
                    break;
            }
        }

        /// <summary>
        /// Capitalizes the first letter of each words in a given the string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        private static string CapitalizeString(string str)
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
        private static int CountLines(string str)
        {
            return str.Length - str.ReplaceLineEndings("").Length + 1;
        }

        /// <summary>
        /// Counts the words in a given string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>An int.</returns>
        private static int CountWords(string str)
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
        /// Enables or disables text functions buttons depending if the input text box is empty or not.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == string.Empty)
            {
                TextFunctionsIsEnabled.Value = false;
                LengthTextBox.Text = "0";
                LinesTextBox.Text = "0";
                WordsTextBox.Text = "0";
            }
            else
            {
                TextFunctionsIsEnabled.Value = true;
                LengthTextBox.Text = InputTextBox.Text.Length.ToString();
                LinesTextBox.Text = CountLines(InputTextBox.Text).ToString();
                WordsTextBox.Text = CountWords(InputTextBox.Text).ToString();
            }
        }
    }
}