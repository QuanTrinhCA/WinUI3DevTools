using Microsoft.UI.Xaml.Controls;
using RandomDataGenerator.FieldOptions;
using System;
using System.Threading.Tasks;
using Windows.Globalization.NumberFormatting;
using WinUI3DevTools.Models;

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
        public ObservableBoolean WordsOptionsGridIsVisible = new();

        public TextGeneratorPage()
        {
            InitializeComponent();
        }

        private async void GenerateButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            switch (TextTypeSelectionComboBox.SelectedItem.ToString())
            {
                case "Lorum Ipsum":
                    if (LipsumParagraphsNumberBox.Value == 0 || double.IsNaN(LipsumParagraphsNumberBox.Value))
                    {
                        LipsumParagraphsNumberBox.Value = 1;
                    }
                    OuputTextBox.Text = await GenerateLipsumAsync(
                        (int)LipsumParagraphsNumberBox.Value,
                        ((int)LipsumSeedNumberBox.Value)
                    );
                    break;

                case "Naughty Strings":
                    break;

                case "Text Regex":
                    break;

                case "Words":
                    break;
            }
        }

        private async Task<string> GenerateLipsumAsync(int paragraphs, int seed)
        {
            return await Task.Run(() =>
            {
                FieldOptionsTextLipsum lipsumFieldOptions;
                if (seed < -2147483647 || seed > 2147483647)
                {
                    lipsumFieldOptions = new FieldOptionsTextLipsum
                    {
                        ValueAsString = true,
                        Paragraphs = paragraphs
                    };
                }
                else
                {
                    lipsumFieldOptions = new FieldOptionsTextLipsum
                    {
                        ValueAsString = true,
                        Paragraphs = paragraphs,
                        Seed = seed
                    };
                }
                var generator = RandomDataGenerator.Randomizers.RandomizerFactory.GetRandomizer(
                    lipsumFieldOptions
                );
                var outputString = generator.Generate().Replace(Environment.NewLine, $"{Environment.NewLine}{Environment.NewLine}");
                outputString = outputString.Insert(0, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ");
                return outputString;
            });
        }

        private void TextTypeSelectionComboBox_SelectionChanged(
                            object sender,
            SelectionChangedEventArgs e
        )
        {
            UpdateOptionsGridVisibility((sender as ComboBox).SelectedItem.ToString());
        }

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