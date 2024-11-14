using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.MockData.Tutors;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {

        public FullObservableCollection<User> user { get; set; }

        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
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

        public async Task<LogoutResponse> LogoutAsync(string token)
        {
            try
            {
                var logoutRequest = new LogoutRequest
                {
                    token = token,
                };

                var response = await _userService.LogoutAccount(logoutRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Logout error: {ex.Message}");
            }
        }


        public async Task<RegisterResponse> RegisterUser(RegisterRequest registerRequest)
        {
            try
            {
                var response = await _userService.RegisterAccount(registerRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration error : {ex.Message}");
            }
        }

        public async Task<UpdateEmailResponse> UpdateEmailByUser(string id, UpdateEmailRequest request)
        {
            try
            {
                var response = await _userService.UpdateUserById(id, request);
                return response;

            }catch(Exception ex)
            {
                throw new Exception($"Update email by user error : {ex.Message}");
            }
        }

        public string ValidateInput(RegisterRequest registerRequest)
        {
            if (string.IsNullOrEmpty(registerRequest.Username) ||
                string.IsNullOrEmpty(registerRequest.Password) ||
                string.IsNullOrEmpty(registerRequest.Email) ||
                string.IsNullOrEmpty(registerRequest.FirstName) ||
                string.IsNullOrEmpty(registerRequest.LastName))
            {
                return "Please fill in all the information";
            }

            if (!IsValidUser(registerRequest.Username))
            {
                return "User must be at least 5 characters";
            }

            if (!IsValidEmail(registerRequest.Email))
            {
                return "Email is invalid";
            }

            if (!IsValidPassword(registerRequest.Password))
            {
                return "The password must be at least 8 characters long and include an uppercase letter, a number, and a special character!";
            }

            return null;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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

        private bool IsValidUser(string user)
        {
            return user.Length >= 5;
        }


        public async Task<VerifyResponse> Verify(VerifyRequest request)
        {
            try
            {
                var response = await _userService.VerifyAccount(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Verify error : {ex.Message}");
            }
        }


        public async Task<ResendTokenResponse> ResendVerifyToken(string _id)
        {
            try
            {
                var response = await _userService.ResendVerifyToken(_id);
                return response;
            }catch (Exception ex)
            {
                throw new Exception("Resend Error: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
