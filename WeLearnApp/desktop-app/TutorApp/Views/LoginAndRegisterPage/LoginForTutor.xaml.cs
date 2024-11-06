using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using TutorApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using System.Threading.Tasks;
using Windows.Storage;
using TutorApp.Helpers;
using TutorApp.Models.ForAPI;
using System.Text.Json;
using TutorApp.Models.ForAPI.Request;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.LoginAndRegisterPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginForTutor : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private LoginViewModel _viewModel;

        public LoginForTutor()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _viewModel = new LoginViewModel(_userService);
        }

        private void navigateToPageStudent(object sender, RoutedEventArgs e)
        {
            // Tạo window mới cho Student Login
            var window = _navigationService.NavigateToNewWindow(
                "StudentLoginWindow",  // Window key
                "LoginForStudent"      // Page key
            );
            window.Title = "Student Login";
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Kiểm tra đầu vào
            var validationMessage = _viewModel.ValidateInput(new LoginRequest { username = username, password = password });
            if (validationMessage != null)
            {
                await ShowErrorDialogAsync(validationMessage);
                return;
            }
            

            LoadingOverlay.Visibility = Visibility.Visible;

            // Gọi hàm đăng nhập từ ViewModel
            try
            {
                var response = await _viewModel.LoginAsync(username, password);
                LoadingOverlay.Visibility = Visibility.Collapsed;
                if (response.Success)
                {
                    //// Lưu token vào LocalSettings
                    var jwtTokens = JsonSerializer.Deserialize<JwtToken>(response.Data.ToString());
                    var role = JwtParser.GetRole(jwtTokens.accessToken);
                    if (role != "TUTOR")
                    {
                        await ShowErrorDialogAsync("Please login tutor-account");
                        return;
                    }


                    var localSettings = ApplicationData.Current.LocalSettings;
                    localSettings.Values["accessToken"] = jwtTokens.accessToken;
                    localSettings.Values["refreshToken"] = jwtTokens.refreshToken;

                    // Điều hướng đến DashboardForTutor
                    _navigationService.NavigateTo("DashboardForTutor");
                }
                else
                {
                    if (response.Code == 5004)
                    {
                        await ShowErrorDialogAsync("This account not verified");
                        _navigationService.NavigateTo("PageLoginTokenRequire", response.Data.ToString());

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
            // Navigate trong cùng window hiện tại
            _navigationService.NavigateTo("RegisterForTutor");
        }


    }
}
