using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Services.Interfaces;
using Windows.Foundation;
using Windows.Foundation.Collections;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        private readonly INavigationService _navigationService;

        public Dashboard()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            try
            {
                contentFrame.Navigate(typeof(HomePage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation failed: {ex.Message}");
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var selectedItem = args.SelectedItem as NavigationViewItem;
                if (selectedItem != null)
                {
                    var pageName = selectedItem.Tag.ToString();

                    switch (pageName)
                    {
                        case "HomePage":
                            contentFrame.Navigate(typeof(HomePage));
                            break;
                        case "AccountPage":
                            contentFrame.Navigate(typeof(AccountPage));
                            break;
                        case "TutorPage":
                            contentFrame.Navigate(typeof(TutorPage));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string query = args.QueryText;
            // Implement your search logic here
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // Handle text input change, possibly provide search suggestions
            }
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            // Handle notification button click
            // You can show a notification panel or navigate to the notifications page
        }

        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            // Handle messages button click
            // You can show a messages panel or navigate to the messages page
        }
        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the filter with the selected category
            var selectedCategory = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
        }

    }
}
