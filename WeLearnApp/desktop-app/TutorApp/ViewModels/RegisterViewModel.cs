using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public RegisterViewModel(IUserService userService)
        {
            _userService = userService;
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
                throw new Exception($"Lỗi khi đăng ký: {ex.Message}");
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
                return "Vui lòng nhập đầy đủ thông tin!";
            }

            if (!IsValidEmail(registerRequest.Email))
            {
                return "Email không hợp lệ!";
            }

            if (!IsValidPassword(registerRequest.Password))
            {
                return "Mật khẩu phải có ít nhất 8 ký tự, bao gồm một chữ hoa, một số và một ký tự đặc biệt!";
            }

            return null; 
        }

        private bool IsValidEmail(string email)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}