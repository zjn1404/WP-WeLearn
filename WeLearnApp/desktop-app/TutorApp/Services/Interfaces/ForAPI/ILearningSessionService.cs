using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface ILearningSessionService
    {
        Task<PageResponse<LearningSessionResponse>> GetLearningSessionList(int page, int size);
        Task<LearningSessionResponse> GetLearningSession(string id);
        Task<LearningSessionResponse> CreateLearningSession(LearningSessionCreationRequest request);
        Task DeleteLearningSession(string id);
    }
}
