using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views.HomePage
{
    public sealed partial class CreateSessionPage : Page
    {
        private LearningSessionCreationViewModel _viewModel;
        private readonly ILearningSessionService _learningSessionService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly ILearningMethodService _learningMethodService;

        public CreateSessionPage()
        {
            this.InitializeComponent();
            _learningSessionService = ((App)Application.Current).Services.GetRequiredService<ILearningSessionService>();
            _gradeService = ((App)Application.Current).Services.GetRequiredService<IGradeService>();
            _subjectService = ((App)Application.Current).Services.GetRequiredService<ISubjectService>();
            _learningMethodService = ((App)Application.Current).Services.GetRequiredService<ILearningMethodService>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is LearningSessionCreationViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                _viewModel = new LearningSessionCreationViewModel(_learningSessionService, _gradeService, _learningMethodService, _subjectService);
            }
            DataContext = _viewModel;
            await _viewModel.InitializeAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDate = DatePicker.SelectedDate;
                var selectedTime = TimePicker.SelectedTime;

                if (selectedDate.HasValue && selectedTime.HasValue)
                {
                    var request = new LearningSessionCreationRequest
                    {
                        StartTime = new DateTime(selectedDate.Value.Year, selectedDate.Value.Month, selectedDate.Value.Day,
                                                 selectedTime.Value.Hours, selectedTime.Value.Minutes, selectedTime.Value.Seconds),
                        Duration = (int)DurationNumberBox.Value,
                        Grade = _viewModel.SelectedGrade,
                        Subject = _viewModel.SelectedSubject,
                        LearningMethod = _viewModel.SelectedLearningMethod,
                        Tuition = decimal.Parse(TuitionTextBox.Text)
                    };

                    var response = await _learningSessionService.CreateLearningSession(request);

                    if (response != null)
                    {
                        await ShowErrorDialogAsync("Success", "Add session sucessfully");

                    }
                }
                else
                {
                    await ShowErrorDialogAsync("Failed", "Add session failed");
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

    }
}
