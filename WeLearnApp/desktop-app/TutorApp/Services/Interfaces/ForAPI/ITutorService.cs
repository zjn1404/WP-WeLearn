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


        /// <summary>
        /// Retrieves a paginated list of tutors based on specified filters and authentication token.
        /// </summary>
        /// <param name="page">The page number to retrieve (1-based indexing)</param>
        /// <param name="size">The number of items per page</param>
        /// <param name="filters">Filter criteria for tutors including:
        ///     - City
        ///     - District
        ///     - Subject
        ///     - Gender
        ///     - Other relevant filter parameters
        /// </param>
        /// <param name="token">Authentication token for API authorization</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a PageResponse with:
        ///     - List of tutors matching the filter criteria
        ///     - Total number of pages
        ///     - Total number of items
        ///     - Current page number
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown when the authentication token is invalid or expired</exception>
        /// <exception cref="ArgumentException">Thrown when page or size parameters are invalid</exception>
        public Task<PageResponse<Tutor>> GetListTutorByFilters(int page, int size, FilterTutor filters, string token);



        /// <summary>
        /// Retrieves a paginated list of tutors based on search criteria.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="size">The number of items per page.</param>
        /// <param name="firstName">The first name of the tutor to search for.</param>
        /// <param name="lastname">The last name of the tutor to search for.</param>
        /// <param name="token">The authentication token for API access.</param>
        /// <returns>A paginated list of tutors matching the search criteria.</returns>
        public Task<PageResponse<Tutor>> GetListTutorBySearch(int page, int size, string name, string token);


    }
}
