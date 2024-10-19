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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private readonly INavigationService _navigationService;

        public Register()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Login");
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Password;

            // Check input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) 
                || string.IsNullOrWhiteSpace(password))
            {
                await ShowErrorDialogAsync("Please enter full username, email and password.");
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
                _navigationService.NavigateTo("Home");
            }
            else
            {
                await ShowErrorDialogAsync("Register failed. Please try again.");
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
    }
}
