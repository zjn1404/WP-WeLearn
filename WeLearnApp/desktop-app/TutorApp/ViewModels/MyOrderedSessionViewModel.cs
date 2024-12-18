using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TutorApp.Common;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class MyOrderedSessionViewModel : INotifyPropertyChanged
    {
        private readonly ILearningSessionService _learningSessionService;

        private FullObservableCollection<OrderResponse> _orderedSessions;
        public FullObservableCollection<OrderResponse> OrderedSessions
        {
            get => _orderedSessions;
            set
            {
                _orderedSessions = value;
                OnPropertyChanged(nameof(OrderedSessions));
            }
        }

        private int _currentPage = 1;
        private const int PageSize = 10;
        private bool _hasMoreSessions = true;
        private bool _isLoading;

        public bool CanLoadMore => _hasMoreSessions && !_isLoading;

        public ICommand LoadMoreCommand { get; }

        public MyOrderedSessionViewModel(ILearningSessionService learningSessionService)
        {
            _learningSessionService = learningSessionService;
            OrderedSessions = new FullObservableCollection<OrderResponse>();

            LoadMoreCommand = new RelayCommand(async () => await LoadMoreSessionsAsync(), () => CanLoadMore);

            LoadMoreCommand.Execute(null);
        }

        public async Task LoadMoreSessionsAsync()
        {
            if (!_hasMoreSessions || _isLoading) return;

            try
            {
                _isLoading = true;
                OnPropertyChanged(nameof(CanLoadMore));

                var response = await _learningSessionService.GetMyOrderedLearningSession(_currentPage, PageSize);

                if (response.data != null && response.data.Any())
                {
                    foreach (var session in response.data)
                    {
                        OrderedSessions.Add(session);
                    }

                    _currentPage++;
                    _hasMoreSessions = !(response.currentPage == response.totalPage);
                }
                else
                {
                    _hasMoreSessions = false;
                }
            }
            catch (Exception ex)
            {
                // Log or handle error
                System.Diagnostics.Debug.WriteLine($"Error loading sessions: {ex.Message}");
                _hasMoreSessions = false;
            }
            finally
            {
                _isLoading = false;
                OnPropertyChanged(nameof(CanLoadMore));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}