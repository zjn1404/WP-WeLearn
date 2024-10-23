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
    public class TutorViewModel : INotifyPropertyChanged
    {
        public FullObservableCollection<Tutor> tutors { get; set; }


        public TutorViewModel()
        {
            IDAO dao = new MockDao();
            tutors = new FullObservableCollection<Tutor>(dao.GetTutors());

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
