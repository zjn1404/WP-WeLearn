using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using TutorApp.Services.Interfaces;
using TutorApp.Views.HomePage;
using Windows.Storage;
namespace TutorApp.Views
{
    public sealed partial class Home : Page
    {
        private readonly INavigationService _navigationService;

        public Home()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            //if(ApplicationData.Current.LocalSettings.Values["token"] == null)
            //{
            //    _navigationService.NavigateTo("Login");
            //    return;
            //}
            //testTokenBlock.Text = ApplicationData.Current.LocalSettings.Values["token"].ToString();
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

        //private void logoutButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ApplicationData.Current.LocalSettings.Values.Remove("token");
        //    _navigationService.NavigateTo("Login");
        //}


    }
}