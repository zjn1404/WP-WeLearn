using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Globalization;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views.HomePage
{
    public sealed partial class CreateSessionPage : Page
    {
        private LearningSessionViewModel _viewModel;
        private readonly ILearningSessionService _learningSessionService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly ILearningMethodService _learningMethodService;
        private readonly INavigationService _navigationService;

        public CreateSessionPage()
        {
            this.InitializeComponent();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _learningSessionService = ((App)Application.Current).Services.GetRequiredService<ILearningSessionService>();
            _gradeService = ((App)Application.Current).Services.GetRequiredService<IGradeService>();
            _subjectService = ((App)Application.Current).Services.GetRequiredService<ISubjectService>();
            _learningMethodService = ((App)Application.Current).Services.GetRequiredService<ILearningMethodService>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is LearningSessionViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                _viewModel = new LearningSessionViewModel(_learningSessionService, _gradeService, _learningMethodService, _subjectService);
            }
            DataContext = _viewModel;
            await _viewModel.InitializeAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!DatePicker.SelectedDate.HasValue || !TimePicker.SelectedTime.HasValue)
                {
                    await ShowErrorDialogAsync("Failed", "Please select date and time");
                    return;
                }


                var selectedDate = DatePicker.SelectedDate.Value;
                var selectedTime = TimePicker.SelectedTime.Value;
                var tuition = TuitionTextBox.Text;
                var duration = DurationNumberBox.Value;
                var grade = GradeTextBox.SelectedValue.ToString();
                var subject = SubjectTextBox.SelectedValue.ToString();
                var learningMethod = LearningMethodTextBox.SelectedValue.ToString();

                var dateString = selectedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var timeString = $"{selectedTime.Hours:D2}:{selectedTime.Minutes:D2}:{selectedTime.Seconds:D2}";


                // Validate tất cả input
                string validationResult = _viewModel.ValidateInput(
                    tuition,
                    grade,
                    learningMethod,
                    dateString,
                    timeString,
                    duration.ToString(),
                    subject
                );

               
                if (!string.IsNullOrEmpty(validationResult))
                {
                    await ShowErrorDialogAsync("Failed", validationResult);
                    return;
                }

                LoadingOverlay.Visibility = Visibility.Visible;
                LearningSessionCreationRequest request = new LearningSessionCreationRequest
                    {
                        StartTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day,
                                                 selectedTime.Hours, selectedTime.Minutes, selectedTime.Seconds),
                        Duration = (int)DurationNumberBox.Value,
                        Grade = _viewModel.SelectedGrade,
                        Subject = _viewModel.SelectedSubject,
                        LearningMethod = _viewModel.SelectedLearningMethod,
                        Tuition = decimal.Parse(TuitionTextBox.Text)
                    };

                    var response = await _learningSessionService.CreateLearningSession(request);
                LoadingOverlay.Visibility = Visibility.Collapsed;
                    if (response != null)
                    {
                        await ShowErrorDialogAsync("Success", "Add session sucessfully");

                    }
               
           
            }
            catch (Exception ex)
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                    PrimaryButtonText = "OK"
                };
                await contentDialog.ShowAsync();
            }
        }
        private async Task ShowErrorDialogAsync(string type, string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = type,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo("DashboardForTutor");
        }
    }
}
