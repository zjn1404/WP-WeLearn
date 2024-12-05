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
using System.Threading.Tasks;
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
    public sealed partial class TuTorReviews : Page
    {
        public TuTorReviewsViewModel _viewModel { get; set; }
        private IEvaluationService _evaluationService;
        private INavigationService _navigationService;

        public TuTorReviews()
        {
            _evaluationService = ((App)Application.Current).Services.GetRequiredService<IEvaluationService>();
            _viewModel = new TuTorReviewsViewModel(_evaluationService);
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            DataContext = _viewModel;
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string tutorId = e.Parameter as string;

            if (!string.IsNullOrEmpty(tutorId))
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var accessToken = localSettings.Values["accessToken"]?.ToString();
                await _viewModel.InitializeAsync(tutorId, accessToken);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.GoBack();

        }
    }
}
