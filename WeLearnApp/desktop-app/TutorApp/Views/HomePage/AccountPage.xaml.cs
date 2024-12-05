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

            if (ApplicationData.Current.LocalSettings.Values["role"] as string != "TUTOR")
            {
                TutorSection.Visibility = Visibility.Collapsed;
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

                var request = new UpdateProfileRequest
                {
                    firstName = FirstName.Text,
                    lastName = LastName.Text,
                    dob = DateTime.TryParse(Dob.Text, out var dob) ? dob : null,
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
                    Debug.WriteLine($"Loading districts for province code: {selectedProvince.code}");
                    var districts = await _thirdPartyService.GetDistrictList(selectedProvince.code);

                    if (districts != null && districts.Any())
                    {
                        District.ItemsSource = districts;

                        var selectedDistrict = districts.FirstOrDefault(d => d.code == _viewModel.SelectedDistrictCode);
                        if (selectedDistrict != null)
                        {
                            District.SelectedValue = selectedDistrict.code;
                            Debug.WriteLine($"Reselected district with code: {selectedDistrict.code}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No districts found for the selected province");
                        District.ItemsSource = null;
                    }
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
            _navigationService.NavigateTo("Dashboard");
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
