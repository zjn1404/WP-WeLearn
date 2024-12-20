using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.Windows;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using TutorApp.Helpers;
using System.Text.Json;
using TutorApp.Models.ForAPI;
using TutorApp.Models.ForAPI.Request;
using System.Diagnostics;

namespace TutorApp.Views
{
    public sealed partial class Login : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly LoginViewModel _viewModel;
        public Login()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _viewModel = new LoginViewModel(_userService);
            var localSettings = ApplicationData.Current.LocalSettings;

           
            if (localSettings.Values.ContainsKey("username") && localSettings.Values.ContainsKey("password"))
            {
                usernameTextBox.Text = localSettings.Values["username"] as string;
                passwordBox.Password = localSettings.Values["password"] as string;
                rememberCheckBox.IsChecked = true;
            }
            else
            {
                rememberCheckBox.IsChecked = false;
            }

        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            Boolean rememberMe = rememberCheckBox.IsChecked ?? false;



            var validationMessage = _viewModel.ValidateInput(new LoginRequest { username=username,password = password});
            if (validationMessage != null) {
                await ShowErrorDialogAsync(validationMessage);
                return;
            }

           
            LoadingOverlay.Visibility = Visibility.Visible;
           
           
            try
            {
               
                var response = await _viewModel.LoginAsync(username, password);
                LoadingOverlay.Visibility = Visibility.Collapsed;
                if (response.Success)
                {
                    //// Lưu token vào LocalSettings
                    var jwtTokens = JsonSerializer.Deserialize<JwtToken>(response.Data.ToString());
                    var role = JwtParser.GetRole(jwtTokens.accessToken);
                    if(role != "USER")
                    {
                        await ShowErrorDialogAsync("Please login user-account");
                        return;
                    }


                    var localSettings = ApplicationData.Current.LocalSettings;
                    localSettings.Values["accessToken"] = jwtTokens.accessToken;
                    localSettings.Values["refreshToken"] = jwtTokens.refreshToken;
                    if (rememberMe)
                    {
                        localSettings.Values["username"] = username;
                        localSettings.Values["password"] = password;
                    }
                    

                    Debug.WriteLine($"Access Token: {jwtTokens.accessToken}");
                    Debug.WriteLine($"Extracted Role: {role}");
                    Debug.WriteLine($"LocalSettings Role Before Save: {localSettings.Values["role"]}");
                    localSettings.Values["role"] = role;
                    Debug.WriteLine($"LocalSettings Role After Save: {localSettings.Values["role"]}");


                    // Điều hướng đến Dashboard
                    _navigationService.NavigateTo("Dashboard");
                }
                else
                {
                    if(response.Code == 5004)
                    {
                        await ShowErrorDialogAsync("This account not verified");
                        _navigationService.NavigateTo("PageLoginTokenRequire",response.Data.ToString());

                    }
                    else
                    {
                    await ShowErrorDialogAsync(response.Message.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync($"Error: {ex.Message}");
            }
           
        }

        private async Task ShowErrorDialogAsync(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Announcement",
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
            _navigationService.NavigateTo("LoginForTutor");

        }
    }
}