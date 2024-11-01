using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services.Interfaces.ForAPI;
namespace TutorApp.ViewModels
{
    public class LogoutViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        public LogoutViewModel(IUserService userService)
        {
            _userService = userService;
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
