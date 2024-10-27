﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.Windows;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views
{
    public sealed partial class Login : Page
    {
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _viewModel;

        public Login()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _viewModel = new LoginViewModel(((App)Application.Current).Services.GetRequiredService<IUserService>());
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
                    //var localSettings = ApplicationData.Current.LocalSettings;
                    //localSettings.Values["token"] = response.Token;

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
            window.Title = "Đăng nhập cho Gia sư";
        }
    }
}