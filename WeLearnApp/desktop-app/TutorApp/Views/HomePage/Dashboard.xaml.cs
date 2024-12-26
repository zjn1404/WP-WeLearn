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
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage;

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
        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;
        private readonly IThirdPartyService _thirdPartyService;
        private readonly ILearningSessionService _learningSessionService;

        private readonly LogoutViewModel _viewModel;
        private readonly UserProfileViewModel _userProfileViewModel;

        public Dashboard()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _thirdPartyService = ((App)Application.Current).Services.GetRequiredService<IThirdPartyService>(); 

            _viewModel = new LogoutViewModel(_userService);
            _userProfileViewModel = new UserProfileViewModel(_userService, _tutorService, _thirdPartyService); 
            DataContext = _userProfileViewModel;

            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            try
            {
                await _userProfileViewModel.InitializeAsync();
                contentFrame.Navigate(typeof(SessionPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Initialization failed: {ex.Message}");
                await ShowErrorDialogAsync($"Failed to load profile: {ex.Message}");
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
                            contentFrame.Navigate(typeof(SessionPage));
                            break;
                        case "AccountPage":
                            contentFrame.Navigate(typeof(AccountPage), _userProfileViewModel);
                            break;
                        case "TutorPage":
                            contentFrame.Navigate(typeof(TutorPage));
                            break;
                        case "MyOrderedSessionPage":
                            contentFrame.Navigate(typeof(MyOrderedSessionPage));
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

        private async void DropdownOptionLogOut_Select(object sender, RoutedEventArgs e)
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                var accessToken = localSettings.Values["accessToken"] as string;
                var response = await _viewModel.LogoutAsync(accessToken);
                if (response.Success)
                {
                    localSettings.DeleteContainer("accessToken");
                    localSettings.DeleteContainer("refreshToken");

                    // Điều hướng đến Dashboard
                    _navigationService.NavigateTo("Login");
                }
                else
                {
                    await ShowErrorDialogAsync("Đăng xuất không thành công. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void DropdownOptionProfile_Select(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(AccountPage));

        }
        private async Task ShowErrorDialogAsync(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Lỗi",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }


    }
}
