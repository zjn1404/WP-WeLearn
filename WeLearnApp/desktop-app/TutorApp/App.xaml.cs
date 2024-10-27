using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using TutorApp.Services;
using TutorApp.Helpers;
using System;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp
{
    public partial class App : Application
    {

        public IServiceProvider Services { get; private set; }
        public new static App Current => (App)Application.Current;
        private Window m_window;
        private Frame rootFrame;

        public App()
        {
            this.InitializeComponent();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var baseUrl = "http://localhost:8080";

            // Core services
            services.AddHttpClient();
            services.AddSingleton<INavigationService>(sp => new NavigationService(rootFrame));
            services.AddSingleton<IUserService>(new UserService(baseUrl));

            // Application services
            services.AddTransient<IAuthenticationService, AuthenticationService>();
           

            // Register ViewModels if needed
            // services.AddTransient<HomeViewModel>();
            // Add other ViewModels as needed

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            rootFrame = ((MainWindow)m_window).ContentFrame;

            Services = ConfigureServices();

          

            var navigationService = Services.GetRequiredService<INavigationService>();
            navigationService.RegisterRoutes();

            // Activate window trước khi set active cho navigation
            m_window.Activate();
            navigationService.SetWindowActive((MainWindow)m_window);

            navigationService.NavigateTo("Login");
        }


    }
}