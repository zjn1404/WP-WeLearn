using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TutorApp.Models
{
    public class UserProfile : INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("dob")]
        public DateTime Dob { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        public string LocationId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

