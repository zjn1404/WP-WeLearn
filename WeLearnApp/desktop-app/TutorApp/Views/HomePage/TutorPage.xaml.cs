using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using TutorApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using TutorApp.Services.Interfaces;
using System.Diagnostics;
using TutorApp.Models;
using TutorApp.Helpers;
using TutorApp.Services.Interfaces.ForAPI;

using TutorApp.Services;
using TutorApp.Models.ForAPI;
using System.Windows.Input;
using TutorApp.Models.ForAPI.Request;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    public sealed partial class TutorPage : Page
    {
        public TutorViewModel _viewModel { get; }
        private readonly ITutorService _tutorService;
        private readonly INavigationService _navigationService;

        public TutorPage()
        {
            this.InitializeComponent();
            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _viewModel = new TutorViewModel(_tutorService);
            DataContext = _viewModel;
        }



        private async void TutorFilter_FilterChanged(object sender, Controls.FilterChangedEventArgs e)
        {
            var filters = new FilterTutor
            {
                city = e.Filters.Province,
                district = e.Filters.District,
                street = e.Filters.Street,
                grade = e.Filters.Grade,
                subject = e.Filters.Subject,
                learningMethod = e.Filters.LearningMethod,
                tuition = e.Filters.TuitionRange,
            };

            await _viewModel.GetListTutorByFilters(filters);
        }

        private async void SearchTutor_Click(object sender, RoutedEventArgs e)
        {
            string value = SearchTutorBox.Text;
            if (!string.IsNullOrEmpty(value))
            {
                 await _viewModel.getListTutorBySearch(value);

            }
        }

        private void TutorCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Tutor selectedTutor)
            {
                _navigationService.NavigateTo("DetailTutor", selectedTutor);
            }
        }

    }

}
