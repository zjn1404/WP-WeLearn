using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Role:INotifyPropertyChanged
    {
            
        public string RoleName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
