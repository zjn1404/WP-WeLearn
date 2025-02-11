﻿using System;
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
using TutorApp.Helpers;

namespace TutorApp.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;
        private readonly IThirdPartyService _thirdPartyService;

        private UserProfileResponse _userProfileResponse;
        private TutorSpecificFieldsResponse _tutorSpecificFieldsResponse;
        private string accessToken = ApplicationData.Current.LocalSettings.Values["accessToken"] as string;
        private string role = ApplicationData.Current.LocalSettings.Values["role"] as string;

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
                OnPropertyChanged(nameof(AvatarUrl));
            }
        }

        public string FirstName => UserProfileResponse?.firstName;
        public string LastName => UserProfileResponse?.lastName;
        public DateTime Dob => UserProfileResponse?.dob ?? DateTime.MinValue;
        public string PhoneNumber => UserProfileResponse?.phoneNumber;
        public string City => UserProfileResponse?.location?.city;
        public string District => UserProfileResponse?.location?.district;
        public string Street => UserProfileResponse?.location?.street;
        public string AvatarUrl => UserProfileResponse?.avatarUrl;

        private int _selectedProvinceCode;
        private int _selectedDistrictCode;

        public int SelectedProvinceCode
        {
            get => _selectedProvinceCode;
            set
            {
                _selectedProvinceCode = value != 0 ? value : -1;
                OnPropertyChanged(nameof(SelectedProvinceCode));
                Debug.WriteLine($"SelectedProvinceCode set to: {value}");
            }
        }
        public int SelectedDistrictCode
        {
            get => _selectedDistrictCode;
            set
            {
                _selectedDistrictCode = value != 0 ? value : -1;
                OnPropertyChanged(nameof(SelectedDistrictCode));
                Debug.WriteLine($"SelectedDistrictCode set to: {value}");
            }
        }

        TutorSpecificFieldsResponse TutorSpecificFieldsResponse
        {
            get => _tutorSpecificFieldsResponse;
            set
            {
                _tutorSpecificFieldsResponse = value;
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Degree));
            }
        }

        public string Description => TutorSpecificFieldsResponse?.Description;
        public string Degree => TutorSpecificFieldsResponse?.Degree;

        public UserProfileViewModel(IUserService userService, ITutorService tutorService, IThirdPartyService thirdPartyService)
        {
            _userService = userService;
            _tutorService = tutorService;
            _thirdPartyService = thirdPartyService;

            _selectedProvinceCode = -1;
            _selectedDistrictCode = -1;
        }
        public async Task InitializeAsync()
        {
            await LoadProfileAsync();
        }
        private async Task LoadProfileAsync()
        {
            try
            {
                UserProfileResponse = await GetProfile();
                if (UserProfileResponse == null)
                {
                    Debug.WriteLine("UserProfileResponse is null.");
                    return;
                }

                if (role == "TUTOR") TutorSpecificFieldsResponse = await GetTutorSpecificFields();

                // Load provinces and districts
                await LoadProvincesAsync();
                await LoadDistrictsAsync();

                //Debug.WriteLine("User profile loaded successfully.", UserProfileResponse.location.ToString());
                //Debug.WriteLine("User profile loaded successfully.", UserProfileResponse.location.city);
                //Debug.WriteLine("User profile loaded successfully.", UserProfileResponse.location.district);

                if (UserProfileResponse.location != null)
                {
                    var province = Provinces?.FirstOrDefault(p =>
                        string.Equals(p.name, UserProfileResponse.location.city, StringComparison.OrdinalIgnoreCase));

                    if (province != null)
                    {
                        SelectedProvinceCode = province.code;
                        Debug.WriteLine($"Setting SelectedProvinceCode to: {province.code} for city: {UserProfileResponse.location.city}");
                    }
                    else
                    {
                        Debug.WriteLine($"No matching province found for city: {UserProfileResponse.location.city}");
                    }

                    var district = Districts?.FirstOrDefault(d =>
                            string.Equals(d.name, UserProfileResponse.location.district, StringComparison.OrdinalIgnoreCase));

                    if (district != null)
                    {
                        SelectedDistrictCode = district.code;
                        Debug.WriteLine($"Setting SelectedDistrictCode to: {district.code} for district: {UserProfileResponse.location.district}");
                    }
                    else
                    {
                        Debug.WriteLine($"No matching district found for district: {UserProfileResponse.location.district}");
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred in LoadProfileAsync: {ex.Message}");
            }
        }

        public async Task LoadProvincesAsync()
        {
            try
            {
                Debug.WriteLine("Fetching provinces...");
                if (_thirdPartyService == null)
                {
                    Debug.WriteLine("_thirdPartyService is null!");
                    return;
                }

                var provinceList = await _thirdPartyService.GetProvinceList();
                if (provinceList == null)
                {
                    Debug.WriteLine("Province list returned null");
                    Provinces = new List<Province>();
                    return;
                }

                Provinces = provinceList;
                Debug.WriteLine($"Fetched {Provinces.Count} provinces");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching provinces: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                Provinces = new List<Province>();
            }
        }

        private async Task LoadDistrictsAsync()
        {
            try
            {
                Debug.WriteLine("Fetching districts...");
                if (_thirdPartyService == null)
                {
                    Debug.WriteLine("_thirdPartyService is null!");
                    return;
                }

                var districtList = await _thirdPartyService.GetDistrictList();
                if (districtList == null)
                {
                    Debug.WriteLine("District list returned null");
                    Districts = new List<District>();
                    return;
                }

                Districts = districtList;
                Debug.WriteLine($"Fetched {Districts.Count} districts");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching district: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                Districts = new List<District>();
            }
        }

        public async Task<UserProfileResponse> GetProfile()
        {
            try
            {
                return await _userService.GetMyProfile();
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
                return await _userService.UpdateMyProfile(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Profile loading error: {ex.Message}");
            }
        }
        public async Task<TutorSpecificFieldsResponse> GetTutorSpecificFields()
        {
            try
            {
                Debug.WriteLine("access token", accessToken);
                Debug.WriteLine("claims", JwtParser.GetAllClaims(accessToken));
                Debug.WriteLine("id", JwtParser.GetId(accessToken));
                return await _tutorService.GetTutorSpecificFields(JwtParser.GetId(accessToken));
            }
            catch (Exception ex)
            {
                throw new Exception($"Profile loading error: {ex.Message}");
            }
        }

        public async Task<UpdateProfileResponse> UpdateTutorSpecificFields(UpdateTutorSpecificFieldsRequest request)
        {
            try
            {
                return await _tutorService.UpdateTutorSpecificFields(request);
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
