using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Order:INotifyPropertyChanged
    {
        public string Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string StudentId { get; set; }
        public string TutorId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
