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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Models;
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

        public DetailTutor()
        {
            
            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _viewModel = new TutorViewModel(_tutorService);
           
            
            this.InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.GoBack();
            
        }


        private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }




    }
}
