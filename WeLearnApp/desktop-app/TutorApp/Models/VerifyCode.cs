using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    internal class VerifyCode:INotifyPropertyChanged
    {

        public string Code { get; set; }
        public string UserId { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
