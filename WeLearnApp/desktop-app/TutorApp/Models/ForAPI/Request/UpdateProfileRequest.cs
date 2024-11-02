using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Models.ForAPI.Request
{
    public class UpdateProfileRequest
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? dob { get; set; }
        public string? phoneNumber { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
    }
}
