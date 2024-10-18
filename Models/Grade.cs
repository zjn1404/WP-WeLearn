using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Grade : INotifyPropertyChanged
    {
        public string Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
