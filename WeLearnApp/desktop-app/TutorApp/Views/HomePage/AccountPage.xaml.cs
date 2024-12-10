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
using TutorApp.Helpers;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.UI.Xaml.Media.Imaging;

namespace TutorApp.Views.HomePage
{
    public sealed partial class AccountPage : Page
    {
        public UserProfileViewModel _viewModel { get; set; }
        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;
        private readonly IThirdPartyService _thirdPartyService;
        private StorageFile _selectedAvatarFile;
        private readonly INavigationService _navigationService;
        public AccountPage()
        {
            this.InitializeComponent();
            _userService = ((App)Application.Current).Services.GetRequiredService<IUserService>();
            _tutorService = ((App)Application.Current).Services.GetRequiredService<ITutorService>();
            _navigationService = ((App)Application.Current).Services.GetRequiredService<INavigationService>();
            _thirdPartyService = ((App)Application.Current).Services.GetRequiredService<IThirdPartyService>();
            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is UserProfileViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                _viewModel = new UserProfileViewModel(_userService, _tutorService, _thirdPartyService);
            }

            DataContext = _viewModel;
            await _viewModel.InitializeAsync();

            if (DateTime.TryParse(_viewModel.Dob.ToString(), out DateTime parsedDate))
            {
                Dob.Date = parsedDate;
            }
            else
            {
                Debug.WriteLine("Invalid date format");
            }



            if (_viewModel.Provinces != null && _viewModel.UserProfileResponse?.location?.city != null)
            {
                var province = _viewModel.Provinces.FirstOrDefault(p =>
                    string.Equals(p.name, _viewModel.UserProfileResponse.location.city,
                    StringComparison.OrdinalIgnoreCase));

                if (province != null)
                {
                    City.SelectedItem = province;
                    await LoadDistrictsForProvince(province);

                    if (_viewModel.Districts != null && _viewModel.UserProfileResponse?.location?.district != null)
                    {
                        var district = _viewModel.Districts.FirstOrDefault(d =>
                            string.Equals(d.name, _viewModel.UserProfileResponse.location.district,
                            StringComparison.OrdinalIgnoreCase));

                        if (district != null)
                        {
                            District.SelectedItem = district;
                        }
                    }
                }
            }

            if (ApplicationData.Current.LocalSettings.Values["role"] as string != "TUTOR")
            {
                TutorSection.Visibility = Visibility.Collapsed;
            }

        }

        private async Task LoadDistrictsForProvince(Province province)
        {
            try
            {
                var districts = await _thirdPartyService.GetDistrictList(province.code);
                _viewModel.Districts = districts;
                District.ItemsSource = districts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading districts: {ex.Message}");
            }
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingOverlay.Visibility = Visibility.Visible;
                string _avatarUrl = null;
                if (_selectedAvatarFile != null)
                {
                    CloudinaryUploader cloudinaryUploader = new CloudinaryUploader();
                    _avatarUrl = await cloudinaryUploader.UploadImageAsync(_selectedAvatarFile.Path);
                }

                var selectedDate = Dob.SelectedDate.Value;

                var request = new UpdateProfileRequest
                {
                    firstName = FirstName.Text,
                    lastName = LastName.Text,
                    dob = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day),
                    phoneNumber = PhoneNumber.Text,
                    city = (City.SelectedItem as Province)?.name,
                    district = (District.SelectedItem as District)?.name,
                    street = Street.Text,
                    avatarUrl = _avatarUrl
                };

                Debug.WriteLine($"Update profile request: FirstName: {request.firstName}, LastName: {request.lastName}, " +
                                $"DOB: {request.dob?.ToString("yyyy-MM-dd")}, Telephone: {request.phoneNumber}, " +
                                $"Location: City: {request.city}, District: {request.district}, Street: {request.street}");

                var response = await _viewModel.UpdateProfile(request);

                LoadingOverlay.Visibility= Visibility.Collapsed;

                if (response.Success)
                {
                    await ShowErrorDialogAsync("Announcement", "Update information successfully.");
                }
                else
                {
                    await ShowErrorDialogAsync("Announcement", "Update information failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error", $"Error: {ex.Message}");
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
                try
                {
                    await LoadDistrictsForProvince(selectedProvince);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading districts: {ex.Message}");
                }
            }
        }

        private async void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window as MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            _selectedAvatarFile = await picker.PickSingleFileAsync();
            if (_selectedAvatarFile != null)
            {
                var bitmapImage = new BitmapImage();
                using (var stream = await _selectedAvatarFile.OpenAsync(FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(stream);
                }

                avatarUrl.ImageSource = bitmapImage;
            }
            else
            {
                Console.WriteLine("No file selected.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values["role"] as string == "TUTOR")
            {
                _navigationService.NavigateTo("DashboardForTutor");
            }
            else
            {
                _navigationService.NavigateTo("Dashboard");
            }
        }

        private async void SaveTutorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadingOverlay.Visibility = Visibility.Visible;

                var request = new UpdateTutorSpecificFieldsRequest
                {
                    degree = Degree.Text,
                    description = Description.Text,
                };

                Debug.WriteLine($"Update tutor specific fields request: Degree: {request.degree}, Description: {request.description}");
                var response = await _viewModel.UpdateTutorSpecificFields(request);

                LoadingOverlay.Visibility = Visibility.Collapsed;

                if (response.Success)
                {
                    await ShowErrorDialogAsync("Announcement", "Update information successfully.");
                }
                else
                {
                    await ShowErrorDialogAsync("Announcement", "Update information failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialogAsync("Error", $"Error: {ex.Message}");
            }
        }

    }
}
