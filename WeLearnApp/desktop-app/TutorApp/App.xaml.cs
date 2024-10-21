using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using TutorApp.Services;
using TutorApp.Helpers;
using System;

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

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            rootFrame = new Frame();
            m_window.Content = rootFrame;
            m_window.Activate();

            Services = ConfigureServices();

            var navigationService = Services.GetRequiredService<INavigationService>();
            navigationService.RegisterRoutes();
            navigationService.NavigateTo("Home");  // Assuming you have a Home view
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Core services
            services.AddSingleton<INavigationService>(sp => new NavigationService(rootFrame));
            services.AddHttpClient();

            // Application services
            services.AddTransient<IApiService, ApiService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
          

            // Register ViewModels if needed
            // services.AddTransient<HomeViewModel>();
            // Add other ViewModels as needed

            return services.BuildServiceProvider();
        }
    }
}