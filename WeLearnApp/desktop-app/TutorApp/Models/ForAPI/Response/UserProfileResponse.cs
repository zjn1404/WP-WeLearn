using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Models.ForAPI.Response
{
    public class UserProfileResponse
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? dob { get; set; }
        public string? phoneNumber { get; set; }
        public LocationResponse? location { get; set; }
    }
}
