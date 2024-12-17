using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TutorApp.Models.ForAPI.Response
{
    public class LearningSessionResponse : INotifyPropertyChanged
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        [JsonPropertyName("duration")]
        public long Duration { get; set; }
        [JsonPropertyName("grade")]
        public int Grade { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("learningMethod")]
        public string LearningMethod { get; set; }
        [JsonPropertyName("tuition")]
        public decimal Tuition { get; set; }
        [JsonPropertyName("tutor")]
        public UserProfileResponse Tutor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
