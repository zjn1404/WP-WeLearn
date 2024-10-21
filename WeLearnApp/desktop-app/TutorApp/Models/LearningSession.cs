using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class LearningSession : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string TutorId { get; set; }
        public DateTime StartTime { get; set; }
        public long Duration { get; set; }
        public int GradeId { get; set; }
        public string SubjectName { get; set; }
        public string LearningMethodName { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
