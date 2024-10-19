using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string RoleName { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
