using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Evaluation :INotifyPropertyChanged
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("studentId")]
        public string StudentId { get; set; }
        public string TutorId { get; set; }
        public int Star {  get; set; }
        public string Comment { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
