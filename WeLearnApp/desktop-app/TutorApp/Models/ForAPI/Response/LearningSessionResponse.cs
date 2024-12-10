using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TutorApp.Models.ForAPI.Response
{
    public class LearningSessionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("grade")]
        public int Grade { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("learningMethod")]
        public string LearningMethod { get; set; }
        [JsonProperty("tuition")]
        public decimal Tuition { get; set; }
        [JsonProperty("tutor")]
        public UserProfileResponse Tutor { get; set; }
    }
}
