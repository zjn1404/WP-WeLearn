using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.Request;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.JsonResponse;
using System.Text.Json;

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
                    
                    await ShowDialogAsync("Registered Successfully");
                    await ShowDialogAsync("Please check mail to take OTP");
                    _navigationService.NavigateTo("PageLoginTokenRequire", response.Data.id);
                }
                else
                {
                    await ShowDialogAsync("Registration Failed. Please try again.");
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
