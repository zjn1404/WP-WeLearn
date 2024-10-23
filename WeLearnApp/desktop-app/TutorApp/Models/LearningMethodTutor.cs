using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class LearningMethodTutor:INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string TutorId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
