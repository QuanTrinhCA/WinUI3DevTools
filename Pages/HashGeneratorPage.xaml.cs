using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinUI3DevTools.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HashGeneratorPage : Page
    {
        public ObservableBoolean FileInputIsEnabled = new();
        public ObservableBoolean GenerateButtonIsEnabled = new();
        public ObservableBoolean TextInputIsEnabled = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="HashGeneratorPage"/> class.
        /// </summary>
        public HashGeneratorPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Generates HASH on GenerateHASHButton click and update ui to display the HASH.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void GenerateHASHButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateButtonIsEnabled.Value = false;
            byte[] inputBytes = Array.Empty<byte>();
            byte[] outputBytes = Array.Empty<byte>();
            if (TextInputIsEnabled.Value && InputTextBox.Text != null)
            {
                inputBytes = Encoding.Default.GetBytes(InputTextBox.Text);
            }
            else if (FileInputIsEnabled.Value && InputFileSelectionButton.Tag != null)
            {
                var buffer = await FileIO.ReadBufferAsync(
                    InputFileSelectionButton.Tag as StorageFile
                );
                inputBytes = buffer.ToArray();
            }
            switch (SelectHASHComboBox.SelectedItem.ToString())
            {
                case "MD5":
                    MD5 md5 = MD5.Create();
                    outputBytes = md5.ComputeHash(inputBytes);
                    break;

                case "SHA1":
                    SHA1 sha1 = SHA1.Create();
                    outputBytes = sha1.ComputeHash(inputBytes);
                    break;

                case "SHA256":
                    SHA256 sha256 = SHA256.Create();
                    outputBytes = sha256.ComputeHash(inputBytes);
                    break;

                case "SHA384":
                    SHA384 sha384 = SHA384.Create();
                    outputBytes = sha384.ComputeHash(inputBytes);
                    break;

                case "SHA512":
                    SHA512 sha512 = SHA512.Create();
                    outputBytes = sha512.ComputeHash(inputBytes);
                    break;
            }
            StringBuilder stringBuilder = new();
            foreach (var outputByte in outputBytes)
            {
                stringBuilder.Append(outputByte.ToString("x2"));
            }
            OutputTextBox.Text = stringBuilder.ToString();
        }

        /// <summary>
        /// Update input selection ui when expanding.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void HashGenerationExpander_Expanding(
            Expander sender,
            ExpanderExpandingEventArgs args
        )
        {
            InputSelectionRadioButtons.SelectedIndex = 0;
            TextInputIsEnabled.Value = true;
            FileInputIsEnabled.Value = false;
            GenerateButtonIsEnabled.Value = false;
        }

        /// <summary>
        /// Processes file input on button click and update ui.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void InputFileSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var file = await OpenFilePicker();
            if (file == null)
            {
                return;
            }
            if (file == ((sender as Button).Tag as StorageFile))
            {
                return;
            }
            (sender as Button).Content = file.Name;
            (sender as Button).Tag = file;
            GenerateButtonIsEnabled.Value = true;
        }

        /// <summary>
        /// Enable generate HASH button when input text changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == null)
            {
                return;
            }
            if ((sender as TextBox).Text == "")
            {
                return;
            }
            GenerateButtonIsEnabled.Value = true;
        }

        /// <summary>
        /// Opens the file picker.
        /// </summary>
        /// <returns><![CDATA[Task<StorageFile>]]></returns>
        private async Task<StorageFile> OpenFilePicker()
        {
            var filePicker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            filePicker.FileTypeFilter.Add("*");
            return await filePicker.PickSingleFileAsync();
        }

        /// <summary>
        /// Update ui to math with input selection.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Tag.ToString())
            {
                case "Text":
                    TextInputIsEnabled.Value = true;
                    FileInputIsEnabled.Value = false;
                    break;

                case "File":
                    TextInputIsEnabled.Value = false;
                    FileInputIsEnabled.Value = true;
                    break;
            }
        }
    }
}
