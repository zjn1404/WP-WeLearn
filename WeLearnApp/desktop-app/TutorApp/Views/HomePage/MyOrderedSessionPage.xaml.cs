using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views.HomePage
{
    public sealed partial class MyOrderedSessionPage : Page
    {
        public MyOrderedSessionViewModel ViewModel { get; }

        public MyOrderedSessionPage()
        {
            this.InitializeComponent();
            var learningSessionService = App.Current.Services.GetService<ILearningSessionService>();
            var navigationService = App.Current.Services.GetService<INavigationService>();
            ViewModel = new MyOrderedSessionViewModel(learningSessionService);
            this.DataContext = ViewModel;
        }

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

        private async void OnSessionCardClicked(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine($"Clicked item: {e.ClickedItem}");
            if (e.ClickedItem is OrderResponse order)
            {
                var session = order.orderDetail.learningSession;

                SelectedSession = session;
                SelectedSession.Tutor = order.tutor;
                SessionDetailsDialog.DataContext = null;
                SessionDetailsDialog.DataContext = this;
                await SessionDetailsDialog.ShowAsync();
            }
        }

        private async void OnProceedToJoinClassroom(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}