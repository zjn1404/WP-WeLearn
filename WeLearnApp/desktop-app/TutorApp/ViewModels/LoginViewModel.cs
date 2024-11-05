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

        public string ValidateInput(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.username) ||
                string.IsNullOrEmpty(loginRequest.password) )
            {
                return "Please fill in all the information";
            }

            if (!IsValidUser(loginRequest.username))
            {
                return "User must be at least 5 characters";
            }


            if (!IsValidPassword(loginRequest.password))
            {
                return "The password must be at least 8 characters long and include an uppercase letter, a number, and a special character!";
            }

            return null;
        }

        private bool IsValidUser(string user)
        {
            return user.Length >= 5;
        }


        private bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSpecialChar = new Regex(@"[!@#$%^&*(),.?""':{}|<>]+");

            return password.Length >= 8 &&
                   hasNumber.IsMatch(password) &&
                   hasUpperChar.IsMatch(password) &&
                   hasLowerChar.IsMatch(password) &&
                   hasSpecialChar.IsMatch(password);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
