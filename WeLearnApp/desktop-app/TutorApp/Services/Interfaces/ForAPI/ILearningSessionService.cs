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
    /// <summary>
    /// The interface defines for crud learning sessions.
    /// </summary>
    public interface ILearningSessionService
    {
        /// <summary>
        /// Asynchronously retrieves a paginated list of learning sessions.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="size">The number of learning sessions per page.</param>
        /// <returns>A `Task` representing the asynchronous operation, with a `PageResponse<LearningSessionResponse>` object</returns>
        Task<PageResponse<LearningSessionResponse>> GetLearningSessionList(int page, int size);

        /// <summary>
        /// Asynchronously retrieves a specific learning session by its ID.
        /// </summary>
        /// <param name="id">The ID of the learning session to retrieve.</param>
        /// <returns>A `Task` representing the asynchronous operation, with a `LearningSessionResponse` object</returns>
        Task<LearningSessionResponse> GetLearningSession(string id);

        /// <summary>
        /// Asynchronously creates a new learning session.
        /// </summary>
        /// <param name="request">The request object containing the data for the new learning session.</param>
        /// <returns>A `Task` representing the asynchronous operation, with a `LearningSessionResponse` object</returns>
        Task<LearningSessionResponse> CreateLearningSession(LearningSessionCreationRequest request);

        /// <summary>
        /// Asynchronously deletes a learning session by its ID.
        /// </summary>
        /// <param name="id">The ID of the learning session to delete.</param>
        /// <returns>A `Task` representing the asynchronous operation.</returns>
        Task DeleteLearningSession(string id);
    }

}
