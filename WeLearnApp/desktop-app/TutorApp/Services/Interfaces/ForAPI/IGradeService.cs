using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;

namespace TutorApp.Services.Interfaces.ForAPI
{
    /// <summary>
    /// The interface defines for getting grade.
    /// </summary>
    public interface IGradeService
    {
        /// <summary>
        /// Asynchronously retrieves a list of all available grade.
        /// </summary>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `List<Grade>` object.
        /// </returns>
        Task<List<Grade>> GetGrades();
    }
}
