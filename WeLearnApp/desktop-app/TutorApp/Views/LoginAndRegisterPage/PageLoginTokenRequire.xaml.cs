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
using TutorApp.Models.ForAPI;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.LoginAndRegisterPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageLoginTokenRequire : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private UserViewModel UserViewModel { get; set; }
        private string _id;
        private bool isDropdownOpen = false;
        private string token;


        public PageLoginTokenRequire()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            UserViewModel = new UserViewModel(_userService);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.GoBack();
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

        //nhận dữ liệu từ page khác
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

           
            if (e.Parameter is string token)
            {
                _id = token;
            }
        }


        private async void Verify_Click(object sender, RoutedEventArgs e)
        {
            string yourToken = yourTokenBox.Text.Trim();

            var request = new VerifyRequest
            {
                code = yourToken,
                userId = _id,
            };
            
            LoadingOverlay.Visibility = Visibility.Visible;

            var isVerify = await UserViewModel.Verify(request);

            LoadingOverlay.Visibility = Visibility.Collapsed;
            if (isVerify.Success)
            {
                await ShowErrorDialogAsync("Success. Please login again");

                _navigationService.GoBack();
            } else
            {
                await ShowErrorDialogAsync("Failed. Please verify again");
            }

        }


        private async void Resend_Click(object sender, RoutedEventArgs e)
        {

            LoadingOverlay.Visibility = Visibility.Visible;
            var response =  await UserViewModel.ResendVerifyToken(_id);
            LoadingOverlay.Visibility = Visibility.Collapsed;
            if (!response.IsSuccess)
            {
                await ShowErrorDialogAsync("Resend Error. Please try again");
            } else
            {
                await ShowErrorDialogAsync("Resend successfully. Please check your mail");
            }
        }

     

        private async void EmailDropdownButton_Click(object sender, RoutedEventArgs e)
        {
            isDropdownOpen = !isDropdownOpen;

            if (isDropdownOpen)
            {
                EmailDropdownContent.Visibility = Visibility.Visible;
                ShowDropdownAnimation.Begin();


                LoadingOverlay.Visibility = Visibility.Visible;
                var response = await UserViewModel.GetTokenUnverifiedEmail(_id);
                LoadingOverlay.Visibility = Visibility.Collapsed;

                if (response.isSuccess)
                {
                    token = response.token;
                }
            }
            else
            {
                EmailDropdownContent.Visibility = Visibility.Collapsed;
                EmailDropdownContent.Opacity = 0;
     
            }
        }

        private async void UpdateEmail_Click(object sender, RoutedEventArgs e)
        {
            string newEmail = EmailBox.Text.Trim();

            if (String.IsNullOrEmpty(newEmail))
            {
                await ShowErrorDialogAsync("Email text box not empty");
            }

            if (!UserViewModel.IsValidEmail(newEmail))
            {
                await ShowErrorDialogAsync("Email is invalid");
            }


            LoadingOverlay.Visibility = Visibility.Visible;
            var response = await UserViewModel.UpdateEmailByUser(new UpdateEmailRequest { email = newEmail, token = token });
            LoadingOverlay.Visibility = Visibility.Collapsed;    

            await ShowErrorDialogAsync(response.StatusMessage);
       
            
            isDropdownOpen = false;
            EmailDropdownContent.Visibility = Visibility.Collapsed;
            EmailDropdownContent.Opacity = 0;
        }

      
    }
}
