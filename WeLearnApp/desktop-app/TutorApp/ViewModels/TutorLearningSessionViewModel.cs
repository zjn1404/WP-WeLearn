using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.MockData.Tutors;
using TutorApp.MockData;

namespace TutorApp.ViewModels
{
    public class TutorLearningSessionViewModel : INotifyPropertyChanged
    {
        private readonly ILearningSessionService _learningSessionService;
        public FullObservableCollection<LearningSessionResponse> learningSessions { get; set; }

        public TutorLearningSessionViewModel(ILearningSessionService learningSessionService)
        {
            _learningSessionService = learningSessionService;
            LoadLearningSessionsAsync();
        }

        private async void LoadLearningSessionsAsync()
        {
            var learningSessionResponses = await _learningSessionService.GetMyLearningSessionList();
            var learningSessionList = learningSessionResponses.Select(response => new LearningSessionResponse
            {
                Id = response.Id,
                Tutor = response.Tutor,
                StartTime = response.StartTime,
                Duration = response.Duration,
                Grade = response.Grade,
                Subject = response.Subject,
                LearningMethod = response.LearningMethod,
                Tuition = response.Tuition
            }).ToList();

            learningSessions = new FullObservableCollection<LearningSessionResponse>(learningSessionList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
