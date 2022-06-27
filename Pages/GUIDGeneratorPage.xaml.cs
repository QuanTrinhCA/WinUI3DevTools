using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GUIDGeneratorPage : Page
    {
        public ObservableCollection<string> GUIDHistory = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIDGeneratorPage"/> class.
        /// </summary>
        public GUIDGeneratorPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Copies the history entry to clipboard on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CopySingleHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dataPackage = new() { RequestedOperation = DataPackageOperation.Copy };
            dataPackage.SetText((sender as AppBarButton).Tag.ToString());
            Clipboard.SetContent(dataPackage);
        }

        /// <summary>
        /// Generates new GUID, add it to history list and display it on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void GenerateGUIDButton_Click(object sender, RoutedEventArgs e)
        {
            GUIDHistory.Add(Guid.NewGuid().ToString());
            GUIDOutputTextBox.Text = GUIDHistory.Last();
        }

        /// <summary>
        /// Set output textbox to GUID entry selected in history list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void GUIDHistoryListView_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            GUIDOutputTextBox.Text = (sender as ListView).SelectedItem.ToString();
        }

        /// <summary>
        /// Removes the GUID entry from history list on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RemoveSingleHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            GUIDHistory.Remove((sender as AppBarButton).Tag.ToString());
        }
    }
}