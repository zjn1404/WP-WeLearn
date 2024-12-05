using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class EvaluationRequest
    {
        public String tutorId { get; set; }
        public int star { get; set; }
        public string comment { get; set; }
    }
}
