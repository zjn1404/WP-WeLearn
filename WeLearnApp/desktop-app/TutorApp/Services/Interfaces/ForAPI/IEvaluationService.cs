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
    /// <summary>
    /// Interface for Evaluation Service
    /// </summary>
    public interface IEvaluationService
    {
        /// <summary>
        /// Retrieves all evaluations for a specific tutor.
        /// </summary>
        /// <param name="tutorId">The ID of the tutor.</param>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="size">The number of evaluations per page.</param>
        /// <param name="token">The authentication token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a page response of evaluation responses.</returns>
        public Task<PageResponse<EvaluationResponse>> getAllEvaluation(string tutorId, int page, int size, string token);

        /// <summary>
        /// Submits an evaluation for a tutor.
        /// </summary>
        /// <param name="request">The evaluation request containing the evaluation details.</param>
        /// <param name="token">The authentication token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the evaluation response.</returns>
        public Task<EvaluationResponse> evaluate(EvaluationRequest request, string token);
    }
}
