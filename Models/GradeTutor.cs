using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class GradeTutor:INotifyPropertyChanged
    {
        public int Id {  get; set; }
        public string TutorId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
