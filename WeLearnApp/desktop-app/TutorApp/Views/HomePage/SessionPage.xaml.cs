using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Models;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Diagnostics;

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
        private readonly IPaymentService _paymentService;

        public SessionPage()
        {
            this.InitializeComponent();
            var navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _paymentService = ((App)Application.Current).Services.GetRequiredService<IPaymentService>();

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

        private async void OnProceedToPayment(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                string amount = SelectedSession?.Tuition.ToString() ?? "0";
                string token = ApplicationData.Current.LocalSettings.Values["accessToken"] as string;

                string paymentUrl = await _paymentService.CreatePayment(amount, token);
                Debug.WriteLine("paymentUrl", paymentUrl);
                if (!string.IsNullOrEmpty(paymentUrl))
                {
                    var uri = new Uri(paymentUrl);
                    Debug.WriteLine("uri", uri);
                    await Windows.System.Launcher.LaunchUriAsync(uri);
                }
                else
                {
                    ContentDialog errorDialog = new ContentDialog
                    {
                        Title = "Payment Error",
                        Content = "Unable to create payment. Please try again.",
                        CloseButtonText = "Close",
                        XamlRoot = this.XamlRoot 
                    };
                    await errorDialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                    CloseButtonText = "Close",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }

    }
}
