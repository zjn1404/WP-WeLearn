﻿using System;
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
            var localSettings = ApplicationData.Current.LocalSettings;
            var accessToken = localSettings.Values["accessToken"]?.ToString();
            var response = await _tutorService.getListTutor(CurrentPage, PerPage, accessToken);


            Tutors.Clear();
            foreach (var tutor in response.data)
            {
                Tutors.Add(tutor);
            }

            Console.WriteLine(Tutors);

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
            var localSettings = ApplicationData.Current.LocalSettings;
            var accessToken = localSettings.Values["accessToken"]?.ToString();
            var response = await _tutorService.GetListTutorByFilters(CurrentPage,PerPage,filters, accessToken);


            if(response != null)
            {
                Tutors.Clear();
                foreach (var tutor in response.data)
                {
                    Tutors.Add(tutor);
                }

                Console.WriteLine(Tutors);
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
            
            var localSettings = ApplicationData.Current.LocalSettings;
            var accessToken = localSettings.Values["accessToken"]?.ToString();
            PageResponse<Tutor> response = await _tutorService.GetListTutorBySearch(CurrentPage, PerPage, name, accessToken); 

            if (response != null)
            {
                Tutors.Clear();
                foreach (var tutor in response.data)
                {
                    Tutors.Add(tutor);
                }

                Console.WriteLine(Tutors);
                TotalPages = response.totalPage;
            }


        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error load tutors by searching: {ex.Message}");
        }
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
