using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Models
{
    public class Tutor : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }

        public string email { get; set; }
        public LocationResponse location { get; set; }
        public string phoneNumber { get; set; }

        public string avatarUrl { get; set; }

        public string? degree { get; set; }
        public string? description { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
      
    }
}
