using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface IEvaluationService
    {
        public Task<PageResponse<EvaluationResponse>> getAllEvaluation(string tutorId, int page, int size);
        public Task<EvaluationResponse> evaluate(EvaluationRequest request);
    }
}
