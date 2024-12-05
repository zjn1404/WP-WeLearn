using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class TutorDetail: INotifyPropertyChanged
    {
        public string degree {  get; set; }
        public string description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
