using System;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    public class JsonResponseUserProfile
    {
        public int code { get; set; }
        public UserProfileData data { get; set; }
    }

    public class UserProfileData 
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dob { get; set; }
        public LocationResponse? location { get; set; }
    }
}
