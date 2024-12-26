using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class LearningSessionViewModel : INotifyPropertyChanged
    {
        private readonly ILearningSessionService _learningSessionService;
        private readonly Microsoft.UI.Dispatching.DispatcherQueue _dispatcherQueue;
        private int _currentPage;
        private int _perPage;
        private int _totalPages;
        private bool _isLoading;
        private LearningSessionResponse _learningSession;

        public LearningSessionResponse LearningSession
        {
            get => _learningSession;
            set
            {
                _learningSession = value;
                OnPropertyChanged(nameof(LearningSession));
            }
        }

        public FullObservableCollection<LearningSessionResponse> LearningSessions { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    LoadSessionCommand.Execute(null);
                }
            }
        }

        public int PerPage
        {
            get => _perPage;
            set
            {
                if (_perPage != value)
                {
                    _perPage = value;
                    OnPropertyChanged(nameof(PerPage));
                }
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));
                }
            }
        }

        public RelayCommand LoadSessionCommand => new RelayCommand(
            async () => await InitializeAsync(),
            () => CanLoad()
        );

        public RelayCommand NextPageCommand => new RelayCommand(
            () => IncreaseCurrentPage(),
            () => CanNavigateNext()
        );

        public RelayCommand PreviousPageCommand => new RelayCommand(
            () => DecreaseCurrentPage(),
            () => CanNavigatePrevious()
        );

        public LearningSessionViewModel(ILearningSessionService learningSessionService)
        {
            _learningSessionService = learningSessionService;
            _dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            LearningSessions = new FullObservableCollection<LearningSessionResponse>();

            _currentPage = 1;
            _perPage = 8;

            _ = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                var pageResponse = await _learningSessionService.GetLearningSessionList(CurrentPage, PerPage);

                Debug.WriteLine($"Received {pageResponse?.data?.Count ?? 0} sessions");

           
                    LearningSessions.Clear();
                    if (pageResponse?.data != null)
                    {
                        foreach (var session in pageResponse.data)
                        {
                            LearningSessions.Add(session);
                            Debug.WriteLine($"Added session: {session.Subject}");
                        }
                    }
                    TotalPages = pageResponse?.totalPage ?? 1;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching learning sessions: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void IncreaseCurrentPage()
        {
            CurrentPage++;
        }

        private void DecreaseCurrentPage()
        {
            CurrentPage--;
        }

        private bool CanLoad() => CurrentPage > 0 && PerPage > 0;
        private bool CanNavigateNext() => CurrentPage < TotalPages;
        private bool CanNavigatePrevious() => CurrentPage > 1;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
