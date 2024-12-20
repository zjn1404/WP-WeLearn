using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;
using Windows.Storage;
using TutorApp.Helpers;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;

public class TutorViewModel : INotifyPropertyChanged
{
    private readonly ITutorService _tutorService;

    public FullObservableCollection<Tutor> Tutors { get; set; }
    private int _currentPage;
    private int _perPage;
    private int _totalPages;

    private Tutor _tutor;
    public Tutor Tutor
    {
        get => _tutor;
        set
        {
            _tutor = value;
            OnPropertyChanged(nameof(Tutor));
        }
    }

    public int CurrentPage
    {
        get { return _currentPage; }
        set
        {
            if (_currentPage != value)
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                LoadTutorsCommand.Execute(null);
            }
        }
    }

    public int PerPage
    {
        get { return _perPage; }
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
        get { return _totalPages; }
        set
        {
            if (_totalPages != value)
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }
    }

    public RelayCommand LoadTutorsCommand => new RelayCommand(async execute => await LoadTutorsAsync(), canExecute => CanLoadTutors());
    public RelayCommand NextPageCommand => new RelayCommand(execute => increseCurrentPage(), canExecute => CanNavigateNext());
    public RelayCommand PreviousPageCommand => new RelayCommand(execute => decreaseCurrentPage(), canExecute => CanNavigatePrevious());

    public TutorViewModel(ITutorService tutorService)
    {
        Tutors = new FullObservableCollection<Tutor>();
        _tutorService = tutorService;

        _currentPage = 1;
        _perPage = 3;

        _ = LoadTutorsAsync();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private async Task LoadTutorsAsync()
    {
        try
        {
          
            var response = await _tutorService.getListTutor(CurrentPage, PerPage);


            Tutors.Clear();
            foreach (var tutor in response.data)
            {
                Tutors.Add(tutor);
            }


            TotalPages = response.totalPage;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading tutors: {ex.Message}");
        }
    }

    public async Task GetListTutorByFilters(FilterTutor filters)
    {
        try
        {
            
         
            var response = await _tutorService.GetListTutorByFilters(CurrentPage,PerPage,filters);


            if(response != null)
            {
                Tutors.Clear();
                foreach (var tutor in response.data)
                {
                    Tutors.Add(tutor);
                }

               
                TotalPages = response.totalPage;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading tutors: {ex.Message}");
        }
    }

    public async Task getListTutorBySearch(string name)
    {
        try
        {
            
            PageResponse<Tutor> response = await _tutorService.GetListTutorBySearch(CurrentPage, PerPage, name); 

            if (response != null)
            {
                Tutors.Clear();
                foreach (var tutor in response.data)
                {
                    Tutors.Add(tutor);
                }

              
                TotalPages = response.totalPage;
            }


        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error load tutors by searching: {ex.Message}");
        }
    }


    public async Task<TutorDetail> GetDetailTutor(string id)
    {
        try
        {

    
            var response = await _tutorService.GetDetailTutorService(id);
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error load tutors by searching: {ex.Message}");
        }

        return null;
    }

    private void increseCurrentPage()
    {
        CurrentPage++;
    }

    private void decreaseCurrentPage()
    {
        CurrentPage--;
    }

    private bool CanLoadTutors() => CurrentPage > 0 && PerPage > 0;
    private bool CanNavigateNext() => CurrentPage < TotalPages; 
    private bool CanNavigatePrevious() => CurrentPage > 1;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
