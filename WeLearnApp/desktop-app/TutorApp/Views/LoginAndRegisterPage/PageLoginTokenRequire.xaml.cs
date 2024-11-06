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
            
            var isVerify = await UserViewModel.Verify(request);

            if (isVerify.Success)
            {
                await ShowErrorDialogAsync("Success. Please login again");

                _navigationService.GoBack();
            } else
            {
                await ShowErrorDialogAsync("Failed. Please verify again");
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

        private async void Resend_Click(object sender, RoutedEventArgs e)
        {
            var response =  await UserViewModel.ResendVerifyToken(_id);
            if (!response.IsSuccess)
            {
                await ShowErrorDialogAsync("Resend Error. Please try again");
            } else
            {
                await ShowErrorDialogAsync("Resend successfully. Please check your mail");
            }
        }
    }
}
