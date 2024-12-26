using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Services.Interfaces.ForAPI;
using Windows.Storage;

namespace TutorApp.Services
{
    public class TokenService : ITokenService
    {
        private string _accessToken;
        private string _refreshToken;
        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }


        public async Task<string> refreshToken(string url)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            _refreshToken = localSettings.Values["refreshToken"] as string;

            if (string.IsNullOrEmpty(_refreshToken))
            {
                throw new InvalidOperationException("No refresh token found in local settings.");
            }

            try
            {
                var request = new RefreshTokenRequest { refreshToken = _refreshToken };
                var api = url + "/api/auth/refresh";
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(api,content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                if (response.IsSuccessStatusCode)
                {
           
                    var newAccessToken = JsonSerializer.Deserialize<RefreshTokenResponse>(responseData?.data.ToString()).accessToken;
             

                    if (!string.IsNullOrEmpty(newAccessToken))
                    {
                        _accessToken = newAccessToken;

                        localSettings.Values["accessToken"] = newAccessToken;
                   
                        return newAccessToken;
                    }

                    throw new Exception("Failed to refresh token: Missing tokens in response.");
                }
                else
                {
                    throw new Exception($"Token refresh failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during token refresh: {ex.Message}", ex);
            }
        }
    }

    public class RefreshTokenResponse
    {
        public string accessToken { get; set; }
  
    }
    public class RefreshTokenRequest
    {
        public string refreshToken { get; set; }
    }
}
