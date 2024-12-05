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
using TutorApp.Services.Interfaces;


namespace TutorApp.ViewModels
{
    public class LearningSessionViewModel : INotifyPropertyChanged
    {
        public FullObservableCollection<LearningSession> learningSessions { get; set; }


        public LearningSessionViewModel()
        {
            IDAO dao = new MockDao();
            learningSessions = new FullObservableCollection<LearningSession>(dao.GetAllLearningSessions());

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
