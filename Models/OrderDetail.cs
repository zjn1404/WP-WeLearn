using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class OrderDetail:INotifyPropertyChanged
    {
        public string OrderDetailId { get; set; }
        public string LearningSessionId { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
