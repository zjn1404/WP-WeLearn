using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Services.Interfaces;
using TutorApp.Views;

namespace TutorApp.Helpers
{
    public static class NavigationHelper
    {
        public static void RegisterRoutes(this INavigationService navigationService)
        {
            // Register all pages here
            navigationService.RegisterPage("Home", typeof(Home));
            navigationService.RegisterPage("Login", typeof(Login));
            navigationService.RegisterPage("Register", typeof(Register));

            // Add more pages as needed
        }
    }
}
