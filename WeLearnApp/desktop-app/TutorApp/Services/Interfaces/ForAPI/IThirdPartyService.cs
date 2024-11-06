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
    /// The interface defines  methods related to get list province and district,...
    /// </summary>
    public interface IThirdPartyService
    {

        /// <summary>
        /// Get list province in VietNam
        /// </summary>
        /// <returns>A `Task` representing the asynchronous operation, with a `List<Province>` object </returns>
        Task<List<Province>> GetProvinceList();

        /// <summary>
        /// Get list district in VietNam
        /// </summary>
        ///<returns>A `Task` representing the asynchronous operation, with a `List<District>` object </returns>
        Task<List<District>> GetDistrictList();


        /// <summary>
        /// Asynchronously retrieves a list of districts based on the provided province code.
        /// </summary>
        /// <param name="provinceCode">The code of the province</param>
        /// <returns>
        /// A `Task` representing the asynchronous operation, with a `List<District>` object.
        /// </returns>
        Task<List<District>> GetDistrictList(int provinceCode);

    }
}
