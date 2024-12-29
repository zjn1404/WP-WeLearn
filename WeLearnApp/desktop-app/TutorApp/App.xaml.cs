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
using System.Net.Http;

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

            
            var baseUrl = Env.GetString("BASE_URL") ?? "http://localhost:8080";
            Debug.WriteLine("test env", baseUrl);

      
            services.AddHttpClient();
            services.AddSingleton<INavigationService>(sp => new NavigationService(rootFrame));


            services.AddScoped<ITokenService>(provider =>
            {
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new TokenService(httpClient); 
            });

            
            services.AddScoped<HttpService>(provider =>
            {
                var tokenService = provider.GetRequiredService<ITokenService>();
                var navigation = provider.GetService<INavigationService>();
                return new HttpService(baseUrl, tokenService,navigation); 
            });

          
            
            services.AddSingleton<IUserService>(sp => new UserService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<IThirdPartyService, ThirdPartyService>();
            services.AddSingleton<IGradeService, GradeService>();
            services.AddSingleton<ISubjectService>(sp => new SubjectService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<ILearningMethodService, LearningMethodService>();
            services.AddSingleton<ILearningSessionService>(sp => new LearningSessionService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<ITutorService>(sp => new TutorService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<IPaymentService>(sp => new PaymentService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<IEvaluationService>(sp => new EvaluationService(sp.GetRequiredService<HttpService>()));
            services.AddSingleton<IVideoCallService>(sp => new VideoCallService(sp.GetRequiredService<HttpService>()));

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