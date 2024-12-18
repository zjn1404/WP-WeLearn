using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class OrderResponse : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string orderTime { get; set; }
        public string studentId { get; set; }
        public UserProfileResponse tutor { get; set; }
        public OrderDetailResponse orderDetail { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class OrderDetailResponse
        {
            public LearningSessionResponse learningSession { get; set; }
        }
    }
}
