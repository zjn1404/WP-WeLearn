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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp.Views.HomePage
{
    public sealed partial class TutorPage : Page
    {
        public TutorViewModel _viewModel { get; }
        private readonly ITutorService _tutorService;

        public TutorPage()
        {
            this.InitializeComponent();
            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _viewModel = new TutorViewModel(_tutorService);
            DataContext = _viewModel;
        }

       

  
        private void TutorFilter_FilterChanged(object sender, Controls.FilterChangedEventArgs e)
        {
            // Xử lý filter theo tutor
            var filters = e.Filters;

           
            if (filters.Location != "All Locations")
            {
                // Filter by location
            }

            if (filters.Subject != "All Subjects")
            {
                // Filter by subject
            }
        }
    }

}
