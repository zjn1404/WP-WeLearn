using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class LearningSessionCreationRequest
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public long Duration { get; set; }
        public int Grade { get; set; }
        public string Subject { get; set; }
        public string LearningMethod { get; set; }
        public decimal Tuition { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, StartTime: {StartTime}, Duration: {Duration}, Grade: {Grade}, Subject: {Subject}, LearningMethod: {LearningMethod}, Tuition: {Tuition}";
        }
    }
}
