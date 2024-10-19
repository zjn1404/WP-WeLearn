using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services.Interfaces;

namespace TutorApp.Views
{
    public sealed partial class Home : Page
    {
        private readonly INavigationService _navigationService;

        public Home()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
        }

        private void NavigateToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("Login");
        }
    }
}