using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using TutorApp.Services;
using TutorApp.Helpers;
using System;
using TutorApp.Services.Interfaces.ForAPI;
using DotNetEnv;
using System.Diagnostics;
using System.IO; 

namespace TutorApp
{
    public partial class App : Application
    {

        public IServiceProvider Services { get; private set; }
        public new static App Current => (App)Application.Current;
        public static Window m_window;
        private Frame rootFrame;

        public App()
        {
            this.InitializeComponent();
            LoadEnvironmentVariables(); // Load .env file
        }
        private void LoadEnvironmentVariables()
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string envPath = Path.Combine(baseDirectory, ".env");

                if (File.Exists(envPath))
                {
                    Env.Load(envPath);
                    Debug.WriteLine("Environment variables loaded successfully.");
                    Debug.WriteLine($"BASE_URL: {Env.GetString("BASE_URL")}");
                }
                else
                {
                    Debug.WriteLine($"Env file not found at: {envPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load environment variables: {ex.Message}");
            }
        }
        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var baseUrl = Env.GetString("BASE_URL") ?? "http://localhost:8080"; // Use BASE_URL from .env file

            Debug.WriteLine("test env", Env.GetString("BASE_URL"));

            HttpService httpService = new HttpService(baseUrl);

            // Core services
            services.AddHttpClient();
            services.AddSingleton<INavigationService>(sp => new NavigationService(rootFrame));
            services.AddSingleton<IUserService>(new UserService(httpService));
            services.AddSingleton<IThirdPartyService, ThirdPartyService>();
            services.AddSingleton<IGradeService, GradeService>();
            services.AddSingleton<ISubjectService>(new SubjectService(httpService));
            services.AddSingleton<ILearningMethodService, LearningMethodService>();
            services.AddSingleton<ILearningSessionService>(new LearningSessionService(baseUrl));
            services.AddSingleton<ITutorService>(new TutorService(httpService));
            services.AddSingleton<IPaymentService>(new PaymentService(httpService));
            services.AddSingleton<IEvaluationService>(new EvaluationService(httpService));


            // Application services



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