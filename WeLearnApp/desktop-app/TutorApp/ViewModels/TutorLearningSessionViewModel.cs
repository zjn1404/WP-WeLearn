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
        public FullObservableCollection<LearningSession> learningSessions { get; set; }

        public TutorLearningSessionViewModel(ILearningSessionService learningSessionService)
        {
            _learningSessionService = learningSessionService;
            LoadLearningSessionsAsync();
        }

        private async void LoadLearningSessionsAsync()
        {
            var learningSessionResponses = await _learningSessionService.GetMyLearningSessionList();
            var learningSessionList = learningSessionResponses.Select(response => new LearningSession
            {
                Id = response.Id,
                TutorId = response.Tutor.id,
                StartTime = response.StartTime,
                Duration = response.Duration,
                GradeId = response.Grade,
                SubjectName = response.Subject,
                LearningMethodName = response.LearningMethod,
                Tuition = response.Tuition
            }).ToList();

            learningSessions = new FullObservableCollection<LearningSession>(learningSessionList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
