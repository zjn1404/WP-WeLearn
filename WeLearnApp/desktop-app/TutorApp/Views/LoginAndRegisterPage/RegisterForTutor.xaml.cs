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
using TutorApp.Models.ForAPI;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.LoginAndRegisterPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterForTutor : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService userService;
        private RegisterViewModel viewModel;

        public RegisterForTutor()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            viewModel = new RegisterViewModel(userService);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("LoginForTutor");
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các trường nhập liệu
            string username = usernameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Password;
            string confirmPassword = confirmPasswordTextBox.Password;

          
            // Tạo yêu cầu đăng ký
            var registerRequest = new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password,
                FirstName = firstnameTextBox.Text,
                LastName = lastnameTextBox.Text,
                Role = "TUTOR"
            };

            // Kiểm tra đầu vào
            var validationMessage = viewModel.ValidateInput(registerRequest);
            if (password != confirmPassword)
            {
                await ShowDialogAsync("Password doesn't match ConfirmPassword", false);
                return;
            }

            if (validationMessage != null)
            {
                await ShowDialogAsync(validationMessage);
                return;
            }

            // Gọi dịch vụ đăng ký
            try
            {
                var response = await viewModel.RegisterUser(registerRequest);
                if (response.Success)
                {
                    await ShowDialogAsync("Registered Successfully", true);
                }
                else
                {
                    await ShowDialogAsync("Registration failed . Please try again.");
                }
            }
            catch (Exception ex)
            {
                await ShowDialogAsync($"Error: {ex.Message}");
            }
        }

        private async Task ShowDialogAsync(string message, bool navigateAfterDialog = false)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Announcement",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();

            if (navigateAfterDialog)
            {
                _navigationService.NavigateTo("LoginForTutor");
            }
        }
    }
}
