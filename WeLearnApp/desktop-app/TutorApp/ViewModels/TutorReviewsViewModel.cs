using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class TuTorReviewsViewModel : INotifyPropertyChanged
    {
        private readonly IEvaluationService _evaluationService;
        private ObservableCollection<EvaluationResponse> _reviews;
        private int _currentPage = 1;
        private int _totalPages;
        private string _tutorId;
        private string _token;
        private const int PageSize = 5;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EvaluationResponse> Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                // Update CanExecute for page commands
                ((RelayCommand)PreviousPageCommand).RaiseCanExecuteChanged();
                ((RelayCommand)NextPageCommand).RaiseCanExecuteChanged();
            }
        }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged();
                // Ensure CanExecute logic is updated when TotalPages changes
                ((RelayCommand)PreviousPageCommand).RaiseCanExecuteChanged();
                ((RelayCommand)NextPageCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public TuTorReviewsViewModel(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
            Reviews = new ObservableCollection<EvaluationResponse>();

            PreviousPageCommand = new RelayCommand(
                async () => await LoadPageAsync(CurrentPage - 1),
                () => CurrentPage > 1
            );

            NextPageCommand = new RelayCommand(
                async () => await LoadPageAsync(CurrentPage + 1),
                () => CurrentPage < TotalPages
            );
        }

        public async Task InitializeAsync(string tutorId, string token)
        {
            _tutorId = tutorId;
            _token = token;
            await LoadPageAsync(1);
        }

        private async Task LoadPageAsync(int page)
        {
            try
            {
                var pageResponse = await _evaluationService.getAllEvaluation(_tutorId, page, PageSize, _token);

                if (pageResponse != null)
                {
                    CurrentPage = page;
                    TotalPages = pageResponse.totalPage;

                    Reviews.Clear();
                    foreach (var review in pageResponse.data)
                    {
                        Reviews.Add(review);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading reviews: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}