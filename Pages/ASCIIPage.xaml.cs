using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinUI3DevTools.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ASCIIPage : Page
    {
        public ObservableCollection<ASCIIItem> ControlASCIITable = new();
        public ObservableControlSize ExpanderSize = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ASCIIPage"/> class.
        /// </summary>
        public ASCIIPage()
        {
            InitializeComponent();

            SizeChanged += ASCIIPage_SizeChanged;
        }

        /// <summary>
        /// Update expander width to be half of the maingrid's width on size change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ASCIIPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ExpanderSize.Width = ASCIIPageMainGrid.ActualWidth / 2;
        }

        /// <summary>
        /// Update expander width to be half of the maingrid's width once the grid loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ASCIIPageMainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ExpanderSize.Width = ASCIIPageMainGrid.ActualWidth / 2;
        }

        /// <summary>
        /// Forces datagrid to not focus or hgihlight any row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ControlASCIIDataGrid_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            (sender as DataGrid).SelectionChanged -= ControlASCIIDataGrid_SelectionChanged;
            (sender as DataGrid).SelectedItem = null;
            (sender as DataGrid).SelectionChanged += ControlASCIIDataGrid_SelectionChanged;
        }

        /// <summary>
        /// Revert the width of expander to that before expanding.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void Expander_Collapsed(
            Microsoft.UI.Xaml.Controls.Expander sender,
            ExpanderCollapsedEventArgs args
        )
        {
            sender.Width = ExpanderSize.Width;
        }

        /// <summary>
        /// Fetch ASCII data when expanding and defaults width to accomodate the datagrid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private async void Expander_Expanding(
            Microsoft.UI.Xaml.Controls.Expander sender,
            ExpanderExpandingEventArgs args
        )
        {
            sender.Width = double.NaN;
            switch (sender.Name)
            {
                case "ControlASCIIExpander":
                    if (ControlASCIITable.Count == 0)
                    {
                        List<ASCIIItem> temp = await Task.Run(() =>
                        {
                            return ASCII.GetControlASCIITable();
                        });
                        temp.ForEach(x => ControlASCIITable.Add(x));
                    }
                    break;

                case "TextASCIIDataGrid":
                    if (ControlASCIITable.Count == 0)
                    {
                        List<ASCIIItem> temp = await Task.Run(() =>
                        {
                            return ASCII.GetControlASCIITable();
                        });
                        temp.ForEach(x => ControlASCIITable.Add(x));
                    }
                    break;
            }
        }
    }
}