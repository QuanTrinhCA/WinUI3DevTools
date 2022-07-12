using Microsoft.UI.Xaml.Controls;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Globalization.NumberFormatting;
using WinUI3DevTools.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextGeneratorPage : Page
    {
        public DecimalFormatter IntergerRoudingFormatter =
            new()
            {
                IntegerDigits = 1,
                FractionDigits = 0,
                NumberRounder = new IncrementNumberRounder()
                {
                    Increment = 1,
                    RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp
                }
            };

        public ObservableBoolean LipsumOptionsGridIsVisible = new();
        public ObservableBoolean NaughtyStringsOptionsGridIsVisible = new();
        public ObservableBoolean RegexOptionsGridIsVisible = new();
        public ObservableBoolean TextFunctionsIsEnabled = new() { Value = false };
        public ObservableBoolean WordsOptionsGridIsVisible = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TextGeneratorPage"/> class.
        /// </summary>
        public TextGeneratorPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears the output textbox on clear button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ClearAppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            OuputTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Copies the output to clipboard on copy button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CopyAppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            DataPackage dataPackage = new() { RequestedOperation = DataPackageOperation.Copy };
            dataPackage.SetText(OuputTextBox.Text);
            Clipboard.SetContent(dataPackage);
        }

        /// <summary>
        /// Generates selected text types on GenerateButton click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void GenerateButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            switch (TextTypeSelectionComboBox.SelectedItem.ToString())
            {
                case "Lorum Ipsum":
                    if (
                        LipsumParagraphsNumberBox.Value == 0
                        || double.IsNaN(LipsumParagraphsNumberBox.Value)
                    )
                    {
                        LipsumParagraphsNumberBox.Value = 1;
                    }
                    OuputTextBox.Text = await GenerateLipsumAsync(
                        (int)LipsumParagraphsNumberBox.Value,
                        (int)LipsumSeedNumberBox.Value
                    );
                    break;

                case "Naughty Strings":
                    OuputTextBox.Text = await GenerateNaughtyStringsAsync(
                        (
                            NaughtyStringsCategoriesComboBox.SelectedItem as ComboBoxItem
                        ).Tag.ToString(),
                        (int)NaughtyStringsSeedNumberBox.Value
                    );
                    break;

                case "Text Regex":
                    if (RegexPatternTextBox.Text is null or "")
                    {
                        break;
                    }
                    OuputTextBox.Text = await GenerateRegexTextAsync(
                        RegexPatternTextBox.Text,
                        (int)RegexSeedNumberBox.Value
                    );
                    break;

                case "Words":
                    if (double.IsNaN(WordsMinNumberBox.Value) || WordsMinNumberBox.Value == 0)
                    {
                        WordsMinNumberBox.Value = 1;
                    }
                    if (
                        double.IsNaN(WordsMaxNumberBox.Value)
                        || WordsMaxNumberBox.Value <= WordsMinNumberBox.Value
                    )
                    {
                        WordsMaxNumberBox.Value = WordsMinNumberBox.Value;
                    }
                    OuputTextBox.Text = await GenerateWordsAsync(
                        (int)WordsMinNumberBox.Value,
                        (int)WordsMaxNumberBox.Value,
                        (int)WordsSeedNumberBox.Value
                    );
                    break;
            }
        }

        /// <summary>
        /// Generates the lipsum paragraphs asynchronously.
        /// </summary>
        /// <param name="paragraphs">The paragraphs.</param>
        /// <param name="seed">The seed.</param>
        /// <returns><![CDATA[Task<string>]]></returns>
        private async Task<string> GenerateLipsumAsync(int paragraphs, int seed)
        {
            return await Task.Run(() =>
            {
                FieldOptionsTextLipsum lipsumFieldOptions = seed is < (-2147483647) or > 2147483647
                    ? new FieldOptionsTextLipsum
                    {
                        ValueAsString = true,
                        Paragraphs = paragraphs
                    }
                    : new FieldOptionsTextLipsum
                    {
                        ValueAsString = true,
                        Paragraphs = paragraphs,
                        Seed = seed
                    };
                IRandomizerString generator = RandomizerFactory.GetRandomizer(
                    lipsumFieldOptions
                );
                string outputString = generator
                    .Generate()
                    .Replace(Environment.NewLine, $"{Environment.NewLine}{Environment.NewLine}");
                outputString = outputString.Insert(
                    0,
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                );
                return outputString;
            });
        }

        /// <summary>
        /// Generates the naughty strings asynchronously.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="seed">The seed.</param>
        /// <returns><![CDATA[Task<string>]]></returns>
        private async Task<string> GenerateNaughtyStringsAsync(string category, int seed)
        {
            return await Task.Run(() =>
            {
                FieldOptionsTextNaughtyStrings lipsumFieldOptions = seed is < (-2147483647) or > 2147483647
                    ? new FieldOptionsTextNaughtyStrings
                    {
                        ValueAsString = true,
                        Categories = category
                    }
                    : new FieldOptionsTextNaughtyStrings
                    {
                        ValueAsString = true,
                        Categories = category,
                        Seed = seed
                    };
                IRandomizerString generator = RandomizerFactory.GetRandomizer(lipsumFieldOptions);
                return generator.Generate();
            });
        }

        /// <summary>
        /// Generates text with regex asynchronously.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="seed">The seed.</param>
        /// <returns><![CDATA[Task<string>]]></returns>
        private async Task<string> GenerateRegexTextAsync(string pattern, int seed)
        {
            return await Task.Run(() =>
            {
                FieldOptionsTextRegex lipsumFieldOptions = seed is < (-2147483647) or > 2147483647
                    ? new FieldOptionsTextRegex
                    {
                        ValueAsString = true,
                        Pattern = pattern
                    }
                    : new FieldOptionsTextRegex
                    {
                        ValueAsString = true,
                        Pattern = pattern,
                        Seed = seed
                    };
                return RandomizerFactory.GetRandomizer(lipsumFieldOptions).Generate();
            });
        }

        /// <summary>
        /// Generates the lipsum words asynchronously.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <param name="seed">The seed.</param>
        /// <returns><![CDATA[Task<string>]]></returns>
        private async Task<string> GenerateWordsAsync(int min, int max, int seed)
        {
            return await Task.Run(() =>
            {
                FieldOptionsTextWords lipsumFieldOptions = seed is < (-2147483647) or > 2147483647
                    ? new FieldOptionsTextWords
                    {
                        ValueAsString = true,
                        Min = min,
                        Max = max
                    }
                    : new FieldOptionsTextWords
                    {
                        ValueAsString = true,
                        Min = min,
                        Max = max,
                        Seed = seed
                    };
                return RandomizerFactory.GetRandomizer(lipsumFieldOptions).Generate();
            });
        }

        /// <summary>
        /// Disables or enables the copy button depending on the output text box on change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OuputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextFunctionsIsEnabled.Value = !string.IsNullOrWhiteSpace((sender as TextBox).Text);
        }

        /// <summary>
        /// Changes the visibility of specific type options on type selection combo box selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void TextTypeSelectionComboBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            UpdateOptionsGridVisibility((sender as ComboBox).SelectedItem.ToString());
        }

        /// <summary>
        /// Updates the specific type options grid visibility.
        /// </summary>
        /// <param name="textType">The text type.</param>
        private void UpdateOptionsGridVisibility(string textType)
        {
            switch (textType)
            {
                case "Lorum Ipsum":
                    LipsumOptionsGridIsVisible.Value = true;
                    NaughtyStringsOptionsGridIsVisible.Value = false;
                    RegexOptionsGridIsVisible.Value = false;
                    WordsOptionsGridIsVisible.Value = false;
                    break;

                case "Naughty Strings":
                    LipsumOptionsGridIsVisible.Value = false;
                    NaughtyStringsOptionsGridIsVisible.Value = true;
                    RegexOptionsGridIsVisible.Value = false;
                    WordsOptionsGridIsVisible.Value = false;
                    break;

                case "Text Regex":
                    LipsumOptionsGridIsVisible.Value = false;
                    NaughtyStringsOptionsGridIsVisible.Value = false;
                    RegexOptionsGridIsVisible.Value = true;
                    WordsOptionsGridIsVisible.Value = false;
                    break;

                case "Words":
                    LipsumOptionsGridIsVisible.Value = false;
                    NaughtyStringsOptionsGridIsVisible.Value = false;
                    RegexOptionsGridIsVisible.Value = false;
                    WordsOptionsGridIsVisible.Value = true;
                    break;
            }
        }
    }
}