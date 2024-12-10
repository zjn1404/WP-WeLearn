using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Subject: INotifyPropertyChanged
    {
        public string name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
