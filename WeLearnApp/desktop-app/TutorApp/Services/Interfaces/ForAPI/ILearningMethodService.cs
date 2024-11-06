using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;

namespace TutorApp.Services.Interfaces.ForAPI
{
    /// <summary>
    /// The interface defines for getting LearningMethod.
    /// </summary>
    public interface ILearningMethodService
    {
        /// <summary>
        /// Asynchronously retrieves a list of all available learning methods.
        /// </summary>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `List<LearningMethod>` object.
        /// </returns>
        Task<List<LearningMethod>> GetLearningMethods();
    }
}
