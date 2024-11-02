using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.Models.ForAPI.Request;
using Windows.Storage;
using TutorApp.Models;
using TutorApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace TutorApp.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly IThirdPartyService _thirdPartyService;

        private UserProfileResponse _userProfileResponse;
        private string accessToken = ApplicationData.Current.LocalSettings.Values["accessToken"] as string;
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }

        public UserProfileResponse UserProfileResponse
        {
            get => _userProfileResponse;
            set
            {
                _userProfileResponse = value;
                OnPropertyChanged(nameof(UserProfileResponse));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Dob));
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(City));
                OnPropertyChanged(nameof(District));
                OnPropertyChanged(nameof(Street));

            }
        }

        public string FirstName => UserProfileResponse?.firstName;
        public string LastName => UserProfileResponse?.lastName;
        public DateTime Dob => UserProfileResponse?.dob ?? DateTime.MinValue;
        public string PhoneNumber => UserProfileResponse?.phoneNumber;
        public string City => UserProfileResponse?.location?.city;
        public string District => UserProfileResponse?.location?.district;
        public string Street => UserProfileResponse?.location?.street;

        private int _selectedProvinceCode;
        private int _selectedDistrictCode;

        public int SelectedProvinceCode
        {
            get => _selectedProvinceCode;
            set
            {
                _selectedProvinceCode = value;
                OnPropertyChanged(nameof(SelectedProvinceCode));
            }
        }
        public int SelectedDistrictCode
        {
            get => _selectedDistrictCode;
            set
            {
                _selectedDistrictCode = value;
                OnPropertyChanged(nameof(SelectedDistrictCode));
            }
        }

        public UserProfileViewModel(IUserService userService, IThirdPartyService thirdPartyService)
        {
            _userService = userService;
            _thirdPartyService = thirdPartyService;
            LoadProfileAsync();
        }

        private async void LoadProfileAsync()
        {
            UserProfileResponse = await GetProfile();

            Provinces = await _thirdPartyService.GetProvinceList();
            Districts = await _thirdPartyService.GetDistrictList();

            if (UserProfileResponse?.location != null)
            {
                var province = Provinces.FirstOrDefault(p => p.name.Equals(UserProfileResponse.location.city, StringComparison.OrdinalIgnoreCase));
                SelectedProvinceCode = (int)province?.code;

                var district = Districts.FirstOrDefault(d => d.name.Equals(UserProfileResponse.location.district, StringComparison.OrdinalIgnoreCase));
                SelectedDistrictCode = (int)district?.code;
            }
        }

        public async Task<UserProfileResponse> GetProfile()
        {
            try
            {
                return await _userService.GetMyProfile(accessToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Profile loading error: {ex.Message}");
            }
        }

        public async Task<UpdateProfileResponse> UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                Debug.WriteLine("req", request.firstName + request.firstName + request.city + request.district + request.street);
                return await _userService.UpdateMyProfile(accessToken, request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Profile loading error: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
