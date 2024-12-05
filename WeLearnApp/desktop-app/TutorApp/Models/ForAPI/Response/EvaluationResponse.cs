using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class EvaluationResponse
    {
        public int star { get; set; }
        public string comment { get; set; }
        public string studentId { get; set; }
        public string studentName { get; set; }
        public string tutorId {  get; set; }
        public string tutorName { get; set; }
    }
}
