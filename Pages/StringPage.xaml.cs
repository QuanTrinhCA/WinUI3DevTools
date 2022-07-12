using Microsoft.UI.Xaml.Controls;
using System;
using WinUI3DevTools.Classes;

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
                    SystemFunctions.CopyText(OutputTextBox.Text);
                    break;

                case "Encode64":
                    OutputTextBox.Text = StringFunctions.EncodeBase64(InputTextBox.Text);
                    break;

                case "Decode64":
                    try
                    {
                        OutputTextBox.Text = StringFunctions.DecodeBase64(InputTextBox.Text);
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
                    OutputTextBox.Text = StringFunctions.CapitalizeString(InputTextBox.Text);
                    break;
            }
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
                LinesTextBox.Text = StringFunctions.CountLines(InputTextBox.Text).ToString();
                WordsTextBox.Text = StringFunctions.CountWords(InputTextBox.Text).ToString();
            }
        }
    }
}