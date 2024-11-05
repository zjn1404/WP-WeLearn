using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class LearningMethodService : ILearningMethodService
    {
        public Task<List<LearningMethod>> GetLearningMethods()
        {
            var learningMethods = new List<LearningMethod>
            {
                new LearningMethod { Name = "Online" },
                new LearningMethod { Name = "Offline" }
            };
            return Task.FromResult(learningMethods);
        }
    }
}
