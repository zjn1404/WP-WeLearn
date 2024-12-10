using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TutorApp.ViewModels;
using TutorApp.Services;
using Microsoft.UI.Xaml.Navigation;
using TutorApp.Services.Interfaces;
using System.Numerics;
using Sprache;
using TutorApp.Models.ForAPI.Request;

namespace TutorApp.Controls
{
    public sealed partial class TutorFilterControl : UserControl
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;
        public TutorFilterControlVM _viewModel { get; set; }
        private readonly IThirdPartyService _thirdPartyService;
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;
        private readonly ILearningSessionService _learningSessionService;
        private readonly ISubjectService _subjectService;
        private readonly ILearningMethodService _learningMethodService;
        private readonly INavigationService _navigationService;
        private readonly ITutorService _tutorService;
       

        public string SelectedCity { get; set; }
        public string SelectedDistrict { get; set; }
        public string SelectedStreet { get; set; }

        public TutorFilterControl()
        {
            this.InitializeComponent();
            _thirdPartyService = ((App)Application.Current).Services.GetRequiredService<IThirdPartyService>();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _gradeService = ((App)Application.Current).Services.GetRequiredService<IGradeService>();
            _learningSessionService = ((App)Application.Current).Services.GetRequiredService<ILearningSessionService>();
            _gradeService = ((App)Application.Current).Services.GetRequiredService<IGradeService>();
            _subjectService = ((App)Application.Current).Services.GetRequiredService<ISubjectService>();
            _learningMethodService = ((App)Application.Current).Services.GetRequiredService<ILearningMethodService>();
            _viewModel = new TutorFilterControlVM(_learningSessionService,_gradeService,_learningMethodService,_subjectService,_userService,_thirdPartyService);
      
            DataContext = _viewModel;


            isOpen = true;
            isOpenGrades = true;
            isOpenSubjects = true;
            isOpenLearningMethod = true;
           

        }


  


        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseFilterChangedEvent();
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseFilterChangedEvent();
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                RaiseFilterChangedEvent();
            }
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            GradeTextBox.SelectedValue = null;
            SubjectTextBox.SelectedValue = null;
            LearningMethodTextBox.SelectedValue = null;
            TuitionFilter.Text = string.Empty;
           


            CityCombobox.SelectedValue = null;
            DistrictComboBox.SelectedValue = null;
            DistrictComboBox.ItemsSource = null;
            Street.Text = string.Empty;

          
            _viewModel.SelectedProvinceCode = -1;
            _viewModel.SelectedDistrictCode = -1;
            _viewModel.SelectedGrade = -1;
            _viewModel.SelectedLearningMethod = "";
            

            SelectedCity = null;
            SelectedDistrict = null;
            SelectedStreet = null;

            isOpen = true;
            isOpenGrades = true;
            isOpenSubjects = true;
            isOpenLearningMethod = true;

            RaiseFilterChangedEvent();
        }

        private void RaiseFilterChangedEvent()
        {
            try
            {
                var filters = new FilterCriteria
                {
                    Province = (CityCombobox.SelectedItem as Province)?.name,
                    District = (DistrictComboBox.SelectedItem as District)?.name,
                    Street = string.IsNullOrWhiteSpace(Street.Text) ? null : Street.Text,
                    Grade = int.TryParse((GradeTextBox.SelectedItem as Grade)?.Id?.ToString(), out var grade) ? grade : (int?)null, 
                    Subject = (SubjectTextBox.SelectedItem as Subject)?.name,
                    LearningMethod = (LearningMethodTextBox.SelectedItem as LearningMethod)?.Name,
                    TuitionRange = string.IsNullOrWhiteSpace(TuitionFilter.Text) ? (int?)null :
                   (int.TryParse(TuitionFilter.Text, out var tuition) ? tuition : (int?)null),
                };

                Console.WriteLine(LearningMethodTextBox.SelectedItem as string);

                FilterChanged?.Invoke(this, new FilterChangedEventArgs(filters));
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




        private async void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityCombobox.SelectedItem is Province selectedProvince)
            {
                try
                {
                    Debug.WriteLine($"Loading districts for province code: {selectedProvince.code}");
                    var districts = await _thirdPartyService.GetDistrictList(selectedProvince.code);

                    if (districts != null && districts.Any())
                    {
                        DistrictComboBox.ItemsSource = districts;

                        // After loading new districts, try to reselect the saved district code
                        var selectedDistrict = districts.FirstOrDefault(d => d.code == _viewModel.SelectedDistrictCode);
                        if (selectedDistrict != null)
                        {
                            DistrictComboBox.SelectedValue = selectedDistrict.code;
                            Debug.WriteLine($"Reselected district with code: {selectedDistrict.code}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No districts found for the selected province");
                        DistrictComboBox.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading districts: {ex.Message}");
                }
                RaiseFilterChangedEvent();
            }
        }


        private Boolean isOpen = true;
        private async void City_GetListChanged(object sender, object e)
        {

            if (isOpen)
            {
                await _viewModel.LoadProvincesAsync();
                isOpen = false;
            }

        }

        private Boolean isOpenGrades = true;
        private async void Grades_GetListChanged(object sender, object e)
        {
            if (isOpenGrades)
            {
                await _viewModel.LoadGradesAsync();
                isOpenGrades = false;
            }
        }


        private Boolean isOpenSubjects = true;
        private async void Subjects_GetListChanged(object sender, object e)
        {
            if (isOpenSubjects)
            {
                await _viewModel.LoadSubjectsAsync();
                isOpenSubjects = false;
            }
        }


        private Boolean isOpenLearningMethod = true;
        private async void LearningMethod_GetListChanged(object sender, object e)
        {
            if (isOpenLearningMethod)
            {
                await _viewModel.LoadLearningMethodsAsync();
                isOpenSubjects = false;
            }
        }
        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            RaiseFilterChangedEvent();
        }
   }

    public class FilterChangedEventArgs : EventArgs
    {
        public FilterCriteria Filters { get; }

        public FilterChangedEventArgs(FilterCriteria filters)
        {
            Filters = filters;
        }
    }

    public class FilterCriteria
    {
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public int? Grade { get; set; }
        public string? Subject { get; set; }
        public string? LearningMethod { get; set; }
        public int? TuitionRange { get; set; }
        public string? SearchText { get; set; }
    }
}