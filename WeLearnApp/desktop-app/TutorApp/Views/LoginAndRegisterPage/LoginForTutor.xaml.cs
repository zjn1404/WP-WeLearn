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
        public LoginForTutor()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
        }

        private void navigateToPageStudent(object sender, RoutedEventArgs e)
        {
            // Create new window and navigate to LoginForTutor page
            var window = _navigationService.NavigateToNewWindow(
                "StudentLoginWindow",  // Window key
                "LoginForStudent"      // Page key
            );

            // Optional: Set window properties
            window.Title = "Student Login";


        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
