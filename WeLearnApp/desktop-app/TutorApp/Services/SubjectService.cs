using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class SubjectService : ISubjectService
    {
        public Task<List<Subject>> GetSubjects()
        {
            var subjects = new List<Subject>
            {
                new Subject { Name = "Math" },
                new Subject { Name = "Physics" },
                new Subject { Name = "Biology" }
            };
            return Task.FromResult(subjects);
        }
    }
}
