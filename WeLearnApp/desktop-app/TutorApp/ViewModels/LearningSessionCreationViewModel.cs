using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Helpers;
using TutorApp.MockData;
using TutorApp.MockData.Tutors;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using Windows.Networking;


namespace TutorApp.ViewModels
{
    public class LearningSessionCreationViewModel : INotifyPropertyChanged
    {
        public FullObservableCollection<LearningSession> learningSessions { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILearningSessionService _learningSessionService;
        private readonly IGradeService _gradeService;
        private readonly ILearningMethodService _learningMethodService;
        private readonly ISubjectService _subjectService;

        public List<Grade> Grades { get; set; }
        public List<LearningMethod> LearningMethods { get; set; }
        public List<Subject> Subjects { get; set; }




        /// <summary>
        /// this is a method for validation Input
        /// </summary>
        /// <param name="tuition">a string tuition </param>
        /// <param name="grade">a string grade</param>
        /// <param name="learningMethod"> a string learning method</param>
        /// <param name="startDate">a string start date</param>
        /// <param name="startTime">a string start time</param>
        /// <param name="duration">a string duration</param>
        /// <param name="subject">a string subject</param>
        /// <returns>
        /// Return a string if having a error, otherwise, return null
        /// </returns>
        public string ValidateInput(
          string tuition,
          string grade,
          string learningMethod,
          string startDate,
          string startTime,
          string duration,
          string subject)
            {
                if (string.IsNullOrEmpty(tuition?.Trim()) ||
                    string.IsNullOrEmpty(grade?.Trim()) ||
                    string.IsNullOrEmpty(learningMethod?.Trim()) ||
                    string.IsNullOrEmpty(startTime?.Trim()) ||
                    string.IsNullOrEmpty(duration?.Trim()) ||
                    string.IsNullOrEmpty(subject?.Trim()) ||
                    string.IsNullOrEmpty(startDate?.Trim()))
                {
                    return "Please fill in all the information";
                }

                if (!decimal.TryParse(tuition, out decimal tuitionValue) || tuitionValue < 0)
                {
                    return "Tuition must be a valid number greater than or equal to 0";
                }

                if (!int.TryParse(duration, out int durationValue) || durationValue < 0)
                {
                    return "Duration must be a valid number greater than 0 or equal to 0";
                }

                if (!DateTime.TryParse(startDate, out DateTime startDateValue) ||
                    startDateValue.Date < DateTime.Now.Date)
                {
                    return "Start date must be from today onwards";
                }

                if (!DateTime.TryParse(startTime, out DateTime startTimeValue))
                {
                    return "Invalid time format";
                }

                DateTime fullStartDateTime = startDateValue.Date.Add(startTimeValue.TimeOfDay);


                if (fullStartDateTime < DateTime.Now)
                {
                    return "Start date and time must be in the future";
                }

                return null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _selectedGrade;
        private string _selectedSubject;
        private string _selectedLearningMethod;

        public async Task InitializeAsync()
        {
            await LoadGradesAsync();
            await LoadSubjectsAsync();
            await LoadLearningMethodsAsync();
        }
        
        public int SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                _selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
            }
        }
        public string SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged(nameof(SelectedSubject));
            }
        }

        public string SelectedLearningMethod
        {
            get => _selectedLearningMethod;
            set
            {
                _selectedLearningMethod = value;
                OnPropertyChanged(nameof(SelectedLearningMethod));
            }
        }

        public LearningSessionCreationViewModel()
        {
            learningSessions = new FullObservableCollection<LearningSession>();
        }

        public LearningSessionCreationViewModel(ILearningSessionService learningSessionService,
            IGradeService gradeService,
            ILearningMethodService learningMethodService,
            ISubjectService subjectService)
        {
            _gradeService = gradeService;
            _learningMethodService = learningMethodService;
            _subjectService = subjectService;
            _learningSessionService = learningSessionService;

        }

        private async Task LoadGradesAsync()
        {
            try
            {
                if (_gradeService == null)
                {
                    return;
                }

                var grades = await _gradeService.GetGrades();
                if (grades == null)
                {
                    Grades = new List<Grade>();
                    return;
                }

                Grades = grades;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching grades: {ex.Message}");
            }
        }

        private async Task LoadSubjectsAsync()
        {
            try
            {
                if (_subjectService == null)
                {
                    return;
                }

                var subjects = await _subjectService.GetSubjects();
                if (subjects == null)
                {
                    Subjects = new List<Subject>();
                    return;
                }

                Subjects = subjects;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching subjects: {ex.Message}");
            }
        }

        private async Task LoadLearningMethodsAsync()
        {
            try
            {
                if (_learningMethodService == null)
                {
                    return;
                }

                var learningMethods = await _learningMethodService.GetLearningMethods();
                if (learningMethods == null)
                {
                    LearningMethods = new List<LearningMethod>();
                    return;
                }

                LearningMethods = learningMethods;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching subjects: {ex.Message}");
            }
        }

        public async Task<LearningSessionResponse> CreateSession(LearningSessionCreationRequest request)
        {
            try
            {
                return await _learningSessionService.CreateLearningSession(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Profile loading error: {ex.Message}");
            }
        }
    }
}
