using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Views;
using TutorApp.Views.HomePage;
using TutorApp.Views.LoginAndRegisterPage;

namespace TutorApp.Helpers
{
    public static class NavigationHelper
    {
        public static void RegisterRoutes(this INavigationService navigationService)
        {
            // Register all pages here
            navigationService.RegisterPage("Home", typeof(Home));
            navigationService.RegisterPage("HomePage", typeof(HomePage));
            navigationService.RegisterPage("Dashboard", typeof(Dashboard));
            navigationService.RegisterPage("Login", typeof(Login));
            navigationService.RegisterPage("Register", typeof(Register));
            navigationService.RegisterPage("RegisterForTutor", typeof(RegisterForTutor));
            navigationService.RegisterPage("LoginForTutor", typeof(LoginForTutor));
            navigationService.RegisterPage("LoginForStudent",typeof(Login));





            // Add more pages as needed
        }
    }
}
