using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class LearningSessionResponse
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public long Duration { get; set; }
        public int Grade { get; set; }
        public string Subject { get; set; }
        public string LearningMethod { get; set; }
        public decimal Tuition { get; set; }
    }
}
