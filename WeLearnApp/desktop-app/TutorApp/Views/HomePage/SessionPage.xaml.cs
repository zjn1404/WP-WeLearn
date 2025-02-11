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
using TutorApp.Models.ForAPI.Response;
using System.Text.Json;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage : Page, INotifyPropertyChanged
    {
        public LearningSessionViewModel ViewModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private LearningSessionResponse _selectedSession;
        public LearningSessionResponse SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged(nameof(SelectedSession));
            }
        }



        private readonly IPaymentService _paymentService;
        private readonly ILearningSessionService _learningSessionService;

        public SessionPage()
        {
            var navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _paymentService = ((App)Application.Current).Services.GetRequiredService<IPaymentService>();
            _learningSessionService = ((App)Application.Current).Services.GetRequiredService<ILearningSessionService>();

            ViewModel = new LearningSessionViewModel(_learningSessionService);
            DataContext = ViewModel;
          
            this.InitializeComponent();
        }

        private async void OnSessionCardClicked(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine($"Clicked item: {e.ClickedItem}");
            if (e.ClickedItem is LearningSessionResponse session)
            {
                Debug.WriteLine($"Selected session: {session.Subject} at {session.StartTime}");
                Debug.WriteLine($"Selected session: {JsonSerializer.Serialize(session.Tutor.location)}");

                SelectedSession = session;
                SessionDetailsDialog.DataContext = null;
                SessionDetailsDialog.DataContext = this;
                await SessionDetailsDialog.ShowAsync();
            }
        }

        private async void OnProceedToPayment(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                string amount = ((int)SelectedSession?.Tuition).ToString() ?? "0";
                string learningSessionId = SelectedSession?.Id.ToString() ?? "";
     

                string paymentUrl = await _paymentService.CreatePayment(amount, learningSessionId);
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
