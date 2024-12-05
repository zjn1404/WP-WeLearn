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

    /// <summary>
    /// The interface defines navigate-routes
    /// </summary>
    public static class NavigationHelper
    {

        /// <summary>
        /// This method registers a page in the navigation system, allowing the application to navigate to that page.
        /// </summary>
        /// <param name="navigationService">An instance of the NavigationService, which is responsible for managing navigation between pages, including registering pages.</param>
        public static void RegisterRoutes(this INavigationService navigationService)
        {
            // Register all pages here
            navigationService.RegisterPage("Home", typeof(Home));
            navigationService.RegisterPage("HomePage", typeof(HomePage));
            navigationService.RegisterPage("Dashboard", typeof(Dashboard));
            navigationService.RegisterPage("DashboardForTutor", typeof(DashboardForTutor));
            navigationService.RegisterPage("Login", typeof(Login));
            navigationService.RegisterPage("Register", typeof(Register));
            navigationService.RegisterPage("RegisterForTutor", typeof(RegisterForTutor));
            navigationService.RegisterPage("LoginForTutor", typeof(LoginForTutor));
            navigationService.RegisterPage("LoginForStudent", typeof(Login));
            navigationService.RegisterPage("PageLoginTokenRequire", typeof(PageLoginTokenRequire));
            navigationService.RegisterPage("MySessions", typeof(MySessions));
            navigationService.RegisterPage("SessionPage", typeof(SessionPage));


            // Add more pages as needed
            navigationService.RegisterPage("DetailTutor", typeof(DetailTutor));
        }
    }
}
