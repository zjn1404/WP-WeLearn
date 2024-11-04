using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class GradeService : IGradeService
    {
        public Task<List<Grade>> GetGrades()
        {
            var grades = new List<Grade>
            {
                new Grade { Id = "1" },
                new Grade { Id = "2" },
                new Grade { Id = "3" },
                new Grade { Id = "4" },
                new Grade { Id = "5" },
                new Grade { Id = "6" },
                new Grade { Id = "7" },
                new Grade { Id = "8" },
                new Grade { Id = "9" },
                new Grade { Id = "10" },
                new Grade { Id = "11" },
                new Grade { Id = "12" },
            };
            return Task.FromResult(grades);
        }
    }
}
