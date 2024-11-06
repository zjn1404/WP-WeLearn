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
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            
            var validationMessage = _viewModel.ValidateInput(new LoginRequest { username=username,password = password});
            if (validationMessage != null) {
                await ShowErrorDialogAsync(validationMessage);
                return;
            }

            // Gọi hàm đăng nhập từ ViewModel
            try
            {
                var response = await _viewModel.LoginAsync(username, password);
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
            // Tạo cửa sổ mới và điều hướng đến trang LoginForTutor
            var window = _navigationService.NavigateToNewWindow(
                "TutorLoginWindow",  // Khóa cửa sổ
                "LoginForTutor"      // Khóa trang
            );

            // Tùy chọn: Thiết lập thuộc tính cửa sổ
            window.Title = "login For Tutor";
        }
    }
}