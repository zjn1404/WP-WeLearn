using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;

namespace TutorApp.Services.Interfaces.ForAPI
{


    /// <summary>
    /// The interface defines methods related to tutor get information
    /// </summary>
    public interface ITutorService
    {

        /// <summary>
        ///  Asynchronously calls the API to get a tutor information. 
        /// </summary>
        /// <param name="id">id of Tutor</param>
        /// <returns> A `Task` representing the asynchronous operation, with a `Tutor` object </returns>
        public Task<Tutor> getTutorInfo(string id);


        /// <summary>
        /// Retrieves a list of all tutors.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of tutors.</returns>
        public Task<PageResponse<Tutor>> getListTutor(int page, int size, string token);
    }
}
