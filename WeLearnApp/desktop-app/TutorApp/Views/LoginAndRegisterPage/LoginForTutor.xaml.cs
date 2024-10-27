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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await ShowErrorDialogAsync("Vui lòng nhập tên người dùng và mật khẩu.");
                return;
            }

            // Gọi hàm đăng nhập từ ViewModel
            try
            {
                var response = await _viewModel.LoginAsync(username, password);
                if (response.Success)
                {
                    //// Lưu token vào LocalSettings
                    var localSettings = ApplicationData.Current.LocalSettings;
                    localSettings.Values["accessToken"] = response.Tokens.accessToken;
                    localSettings.Values["refreshToken"] = response.Tokens.refreshToken;


                    // Điều hướng đến Dashboard
                    _navigationService.NavigateTo("Home");
                }
                else
                {
                    await ShowErrorDialogAsync("Đăng nhập không thành công. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync($"Đã xảy ra lỗi: {ex.Message}");
            }
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

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate trong cùng window hiện tại
            _navigationService.NavigateTo("RegisterForTutor");
        }


    }
}
