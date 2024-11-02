using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views.HomePage
{
    public sealed partial class AccountPage : Page
    {
        public UserProfileViewModel _viewModel { get; set; }
        private readonly IUserService _userService;
        private readonly IThirdPartyService _thirdPartyService;

        public AccountPage()
        {
            this.InitializeComponent();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _thirdPartyService = ((App)Application.Current).Services.GetRequiredService<IThirdPartyService>();

            _viewModel = new UserProfileViewModel(_userService, _thirdPartyService);
            DataContext = _viewModel;
        }

        // Override OnNavigatedTo for async data loading
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await LoadProvincesAsync();
        }

        private async Task LoadProvincesAsync()
        {
            try
            {
                // Fetch province list from the third-party service
                List<Province> provinces = await _thirdPartyService.GetProvinceList();

                if (provinces != null && provinces.Any())
                {
                    City.ItemsSource = provinces;
                    City.DisplayMemberPath = "name";      // Property to display
                    City.SelectedValuePath = "code";      // Value property for selection
                }
                else
                {
                    Console.WriteLine("Failed to load provinces.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error", $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new UpdateProfileRequest
                {
                    firstName = FirstName.Text,
                    lastName = LastName.Text,
                    dob = DateTime.TryParse(Dob.Text, out var dob) ? dob : null,
                    phoneNumber = PhoneNumber.Text,
                    //location = new LocationRequest
                    
                        city = (City.SelectedItem as Province)?.name,
                        district = (District.SelectedItem as District)?.name, 
                        street = Street.Text
                    
                };

                Debug.WriteLine($"Update profile request: FirstName: {request.firstName}, LastName: {request.lastName}, " +
                                $"DOB: {request.dob?.ToString("yyyy-MM-dd")}, Telephone: {request.phoneNumber}, " +
                                $"Location: City: {request.city}, District: {request.district}, Street: {request.street}");

                var response = await _viewModel.UpdateProfile(request);
                if (response.Success)
                {
                    await ShowErrorDialogAsync("Update user profile successfully", "Cập nhật thông tin thành công.");
                }
                else
                {
                    await ShowErrorDialogAsync("Update user profile failed", "Cập nhật thông tin không thành công. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error", $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private async Task ShowErrorDialogAsync(string type, string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = type,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private async void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (City.SelectedItem is Province selectedProvince)
            {
                // Fetch the list of districts for the selected city
                List<District> districts = await _thirdPartyService.GetDistrictList(selectedProvince.code); // Use the province code here

                if (districts != null && districts.Any())
                {
                    District.ItemsSource = districts;
                    District.DisplayMemberPath = "name"; // Ensure this displays the district names
                    District.SelectedValuePath = "code"; // Set the value property for selection

                    // Attempt to set the SelectedDistrictCode based on the previously fetched user profile data
                    var userDistrictName = _viewModel.UserProfileResponse?.location?.district;

                    if (userDistrictName != null)
                    {
                        // Find the district that matches the user's district name
                        var selectedDistrict = districts.FirstOrDefault(d => d.name.Equals(userDistrictName, StringComparison.OrdinalIgnoreCase));
                        District.SelectedItem = selectedDistrict; // This sets the selected district based on the name
                    }
                }
                else
                {
                    Console.WriteLine("Failed to load districts.");
                    District.ItemsSource = null; // Clear district list if load failed
                }
            }
        }

    }
}
