using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using TutorApp.ViewModels;
using TutorApp.Models.ForAPI;
using System.Threading.Tasks;
using System;
using TutorApp.Services.Interfaces.ForAPI;


namespace TutorApp.Views
{
    public sealed partial class Register : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private RegisterViewModel viewModel;

        public Register()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            viewModel = new RegisterViewModel(_userService);
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các trường nhập liệu
            string username = usernameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Password;

            // Tạo yêu cầu đăng ký
            var registerRequest = new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password,
                FirstName = firstnameTextBox.Text,
                LastName = lastnameTextBox.Text,
                Role = "User"
            };

            // Kiểm tra đầu vào
            var validationMessage = viewModel.ValidateInput(registerRequest);
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
                    await ShowDialogAsync("Đăng ký thành công", true);
                }
                else
                {
                    await ShowDialogAsync("Đăng ký không thành công. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                await ShowDialogAsync($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private async Task ShowDialogAsync(string message, bool navigateAfterDialog = false)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Thông báo",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();

            if (navigateAfterDialog)
            {
                _navigationService.NavigateTo("Login");
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Login");
        }
    }
}