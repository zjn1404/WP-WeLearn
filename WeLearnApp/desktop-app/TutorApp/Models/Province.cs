using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    public class Province
    {
        public int code { get; set; }
        public string name { get; set; }
        public District[]? districts { get; set; }
    }
}
