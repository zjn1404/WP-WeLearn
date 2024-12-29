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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
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
    public sealed partial class MySessions : Page
    {
        private readonly IVideoCallService _videoCallService;
        public TutorLearningSessionViewModel ViewModel { get; }
        public ILearningSessionService learningSessionService;

        public MySessions()
        {
            this.InitializeComponent();
            _videoCallService = ((App)Application.Current).Services.GetRequiredService<IVideoCallService>();
            var navigationService = App.Current.Services.GetService<INavigationService>();
            learningSessionService = App.Current.Services.GetService<ILearningSessionService>();
            ViewModel = new TutorLearningSessionViewModel(learningSessionService);
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private LearningSession _selectedSession;
        public LearningSession SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged(nameof(SelectedSession));
            }
        }

        private async void OnSessionCardClicked(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine($"Clicked item: {e.ClickedItem}");
            if (e.ClickedItem is LearningSession session)
            {
                SelectedSession = session;
                SessionDetailsDialog.DataContext = null;
                SessionDetailsDialog.DataContext = this;
                await SessionDetailsDialog.ShowAsync();
            }
        }


        private async void OnProceedToJoinClassroom(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                string learningSessionId = SelectedSession?.Id.ToString() ?? "";


                string roomUrl = await _videoCallService.JoinRoom(learningSessionId);
                Debug.WriteLine("paymentUrl", roomUrl);
                if (!string.IsNullOrEmpty(roomUrl))
                {
                    var uri = new Uri(roomUrl);
                    Debug.WriteLine("uri", uri);
                    await Windows.System.Launcher.LaunchUriAsync(uri);
                }
                else
                {
                    ContentDialog errorDialog = new ContentDialog
                    {
                        Title = "Joining Room Error",
                        Content = "Unable to join the room. Please try again.",
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
