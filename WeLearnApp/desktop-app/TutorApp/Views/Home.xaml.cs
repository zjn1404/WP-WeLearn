using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;
using Windows.Storage;
namespace TutorApp.Views
{
    public sealed partial class Home : Page
    {
        private readonly INavigationService _navigationService;

        public Home()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            if(ApplicationData.Current.LocalSettings.Values["token"] == null)
            {
                _navigationService.NavigateTo("Login");
                return;
            }
            testTokenBlock.Text = ApplicationData.Current.LocalSettings.Values["token"].ToString();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values.Remove("token");
            _navigationService.NavigateTo("Login");
        }
    }
}