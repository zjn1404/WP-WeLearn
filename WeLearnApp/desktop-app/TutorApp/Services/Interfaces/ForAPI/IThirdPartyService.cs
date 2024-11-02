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
    public interface IThirdPartyService
    {
        Task<List<Province>> GetProvinceList();
        Task<List<District>> GetDistrictList();

        Task<List<District>> GetDistrictList(int provinceCode);

    }
}
