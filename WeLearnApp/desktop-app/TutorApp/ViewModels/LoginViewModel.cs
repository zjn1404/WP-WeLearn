using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

  
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    username = username,
                    password = password
                };

                var response = await _userService.LoginAccount(loginRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login error: {ex.Message}");
            }
        }


        /// <summary>
        /// this is a method for validation Input
        /// </summary>
        /// <param name="loginRequest">Containing password and username</param>
        /// <returns>
        /// Return a string if having a error, otherwise, return null
        /// 
        /// </returns>
        public string ValidateInput(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.username) ||
                string.IsNullOrEmpty(loginRequest.password) )
            {
                return "Please fill in all the information";
            }
            return null;
        }

    
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
