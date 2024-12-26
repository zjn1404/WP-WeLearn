using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface ITokenService
    {   

        /// <summary>
        /// refresh accessToken when itseft exprired
        /// </summary>
        /// <returns>accessToken</returns>
        Task<string> refreshToken(string url);
    }
}
