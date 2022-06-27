using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI3DevTools
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            navigationView.SelectionChanged += NavigationView_SelectionChanged;
            navigationView.BackRequested += NavigationView_BackRequested;

            contentFrame.Navigated += ContentFrame_Navigated;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!contentFrame.CanGoBack) { navigationView.IsBackEnabled = false; return; }
            navigationView.IsBackEnabled = true;
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            contentFrame.GoBack();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected) { contentFrame.Navigate(typeof(Pages.SettingsPage)); return; }
            contentFrame.Navigate(Type.GetType("WinUI3DevTools.Pages." + (args.SelectedItem as NavigationViewItem).Tag.ToString()));
        }
    }
}
