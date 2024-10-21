using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Evaluation :INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string TutorId { get; set; }
        public int Start {  get; set; }
        public string Comment { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
