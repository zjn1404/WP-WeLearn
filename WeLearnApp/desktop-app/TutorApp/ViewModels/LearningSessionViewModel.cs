using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Helpers;
using TutorApp.MockData;
using TutorApp.MockData.Tutors;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;


namespace TutorApp.ViewModels
{
    public class LearningSessionViewModel : INotifyPropertyChanged
    {
        private readonly ILearningSessionService _learningSessionService;
        private readonly Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public FullObservableCollection<LearningSessionResponse> learningSessions { get; set; }

        public LearningSessionViewModel(ILearningSessionService learningSessionService)
        {
            _learningSessionService = learningSessionService;
            dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            learningSessions = new FullObservableCollection<LearningSessionResponse>();

            InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var pageResponse = await _learningSessionService.GetLearningSessionList(1, 20);

                Debug.WriteLine($"Received {pageResponse?.data?.Count ?? 0} sessions");

                dispatcherQueue.TryEnqueue(() =>
                {
                    learningSessions.Clear();
                    if (pageResponse?.data != null)
                    {
                        foreach (var session in pageResponse.data)
                        {
                            learningSessions.Add(session);
                            Debug.WriteLine($"Added session: {session.Subject}");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching learning sessions: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
