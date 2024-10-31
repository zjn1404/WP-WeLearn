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
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAccount(RegisterRequest request);
        Task<LoginResponse> LoginAccount(LoginRequest login);
        Task<LogoutResponse> LogoutAccount(LogoutRequest logout);

        Task<VerifyResponse> VerifyAccount(VerifyRequest request);

        Task<ResendTokenResponse> ResendVerifyToken(string _id);

    }
}
