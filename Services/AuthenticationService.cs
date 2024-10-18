using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Services.Interfaces;

namespace TutorApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        // Thêm các dependency khác nếu cần

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        // Triển khai các phương thức
    }
}
