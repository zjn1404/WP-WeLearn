using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;

namespace TutorApp.Services.Interfaces.ForAPI
{   

    /// <summary>
    /// A interface defines methods to get subject
    /// </summary>
    public interface ISubjectService
    {

        /// <summary>
        /// Asynchronously calls the API to get list subject. 
        ///
        /// </summary>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `List<Subject>` object
        /// </returns>
        Task<List<Subject>> GetSubjects();
    }
}
