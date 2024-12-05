using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class UpdateTutorSpecificFieldsRequest
    {
        public string description { get; set; }
        public string degree { get; set; }
    }
}
