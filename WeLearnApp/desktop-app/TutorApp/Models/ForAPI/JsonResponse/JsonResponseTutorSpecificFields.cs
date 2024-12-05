using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Models.ForAPI.JsonResponse
{
    internal class JsonResponseTutorSpecificFields
    {
        public int code { get; set; }
        public TutorSpecificFieldsData data { get; set; }
    }

    public class TutorSpecificFieldsData
    {
        public string degree { get; set; }
        public string description { get; set; }
    }
}
