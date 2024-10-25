using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace TutorApp.Views
{
    public sealed partial class Login : Page
    {
        private readonly INavigationService _navigationService;

        public Login()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
        }

   

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Check input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await ShowErrorDialogAsync("Please enter both username and password.");
                return;
            }

            // Check valid username and password
            // Call API to check username and password, return code and token
            // APIResponse = await API.Login(username, password);
            string APIResponseCode = "200"; // APIResponse.code();
            string APIResponseToken = "kakahehe"; // APIResponse.token();

            if (APIResponseCode == "200")
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["token"] = APIResponseToken;
                _navigationService.NavigateTo("Dashboard");
            }
            else
            {
                await ShowErrorDialogAsync("Login failed. Please try again.");
            }
        }

        private async Task ShowErrorDialogAsync(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Register");
        }

        private void registerButtonForTutor_Click(object sender, RoutedEventArgs e)
        {
            // Create new window and navigate to LoginForTutor page
            var window = _navigationService.NavigateToNewWindow(
                "TutorLoginWindow",  // Window key
                "LoginForTutor"      // Page key
            );

            // Optional: Set window properties
            window.Title = "Tutor Login";
        }
    }
}