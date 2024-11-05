using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    public class JsonResponseForLearningSession
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public JsonResponseForLearningSessionData Data { get; set; }
    }

    public class JsonResponseForLearningSessionData
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
        public TutorDetails Tutor { get; set; } 
    }

    public class TutorDetails
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public DateTime? Dob { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }
    }

}
