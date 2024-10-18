using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Location: INotifyPropertyChanged
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Street { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
