using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Student : INotifyPropertyChanged
    {
       public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DayOfBirth { get; set; }
        public string LocationId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}
