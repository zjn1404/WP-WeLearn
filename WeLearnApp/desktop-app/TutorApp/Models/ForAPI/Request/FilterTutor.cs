using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class FilterTutor
    {
        public string? city { get; set; }
        public string? district { get; set; }

        public string? street { get; set; }

        public int? grade { get; set; }

        public string? subject { get; set; }

        public string? learningMethod { get; set; }

        public int? tuition { get; set; }
    }
}
