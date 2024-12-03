using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Models;
using TutorApp.Services.Interfaces;
using TutorApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage : Page
    {
        public LearningSessionViewModel ViewModel { get; }
        public LearningSession SelectedSession { get; set; }

        public SessionPage()
        {
            this.InitializeComponent();
            var navigationService = App.Current.Services.GetService<INavigationService>();
            ViewModel = new LearningSessionViewModel();
            DataContext = this;
        }

        private async void OnSessionCardClicked(object sender, ItemClickEventArgs e)
        {
            // Get the clicked session
            if (e.ClickedItem is LearningSession session)
            {
                SelectedSession = session;
                SessionDetailsDialog.DataContext = this; // Bind to this page to access SelectedSession
                await SessionDetailsDialog.ShowAsync();
            }
        }

        private void OnProceedToPayment(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Navigate to payment page or execute payment logic
            // Example: Navigate to a PaymentPage with session details
            //var navigationService = App.Current.Services.GetService<INavigationService>();
            //navigationService?.Navigate(typeof(PaymentPage), SelectedSession);
        }
    }
}
