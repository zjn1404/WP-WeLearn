using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailTutor : Page
    {
        private Tutor tutor;
        public TutorViewModel _viewModel { get; }
        private readonly ITutorService _tutorService;
        private readonly INavigationService _navigationService;
        private readonly IEvaluationService _evaluationService;

        public DetailTutor()
        {

            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _evaluationService = ((App)Application.Current).Services.GetRequiredService<IEvaluationService>();
            _viewModel = new TutorViewModel(_tutorService);


            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Tutor selectedTutor)
            {

                tutor = selectedTutor;
                var response = await _viewModel.GetDetailTutor(tutor.id);
                tutor.degree = response.degree;
                tutor.description = response.description;

                

                DataContext = _viewModel;
                _viewModel.Tutor = tutor;


            }
            base.OnNavigatedTo(e);
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.GoBack();
            
        }


        private async void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            int rating = (int)Rating.Value;
            string reviewText = ReviewTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(reviewText))
            {
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Validation Error",
                    Content = "Please write a review before submitting.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot 
                };
                await errorDialog.ShowAsync();
                return;
            }

            if (rating == 0)
            {
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Validation Error",
                    Content = "Please provide a rating before submitting.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
                return;
            }

            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var accessToken = localSettings.Values["accessToken"]?.ToString();

                if (string.IsNullOrEmpty(accessToken))
                {
                    ContentDialog authErrorDialog = new ContentDialog
                    {
                        Title = "Authorization Error",
                        Content = "You are not logged in. Please log in to submit a review.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await authErrorDialog.ShowAsync();
                    return;
                }

                var evaluationRequest = new EvaluationRequest
                {
                    tutorId = tutor.id,
                    star = rating,
                    comment = reviewText
                };

                var response = await _evaluationService.evaluate(evaluationRequest, accessToken);
                
                if (response != null)
                {
                    ContentDialog successDialog = new ContentDialog
                    {
                        Title = "Review Submitted",
                        Content = "Thank you for your review!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await successDialog.ShowAsync();

                    Rating.Value = 0;
                    ReviewTextBox.Text = string.Empty;
                }
                else
                {
                    ContentDialog failureDialog = new ContentDialog
                    {
                        Title = "Submission Failed",
                        Content = "Unable to submit review. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await failureDialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error submitting review: {ex.Message}");
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = ex.Message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }

        private async void ViewReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            string currentTutorId = this.tutor?.id;

            if (!string.IsNullOrEmpty(currentTutorId))
            {
                Frame.Navigate(typeof(TuTorReviews), currentTutorId);
            }
            else
            {
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "No tutor selected",
                    CloseButtonText = "OK"
                };
                await errorDialog.ShowAsync();
            }
        }

    }
}
