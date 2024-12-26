using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Helpers;
using TutorApp.Services.Interfaces.ForAPI;
using Windows.Storage;
using TutorApp.Services.Interfaces;

namespace TutorApp.Services
{
    public class HttpService
    {

        private readonly string _baseUrl;
        private readonly ITokenService _tokenService;
        private readonly INavigationService _navigationService;

        public HttpService(string baseUrl, ITokenService tokenService, INavigationService navigationService)
        {
            _baseUrl = baseUrl;
            _tokenService = tokenService;
            _navigationService = navigationService;
        }


        public HttpClient CreateClient(string? token = null)
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false, 
                AutomaticDecompression = DecompressionMethods.None, 
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseUrl),
                DefaultRequestHeaders =
            {
                Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
            }
            };

        
            client.DefaultRequestHeaders.Clear();

       
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<HttpClient> AuthenticatedCallAPI()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var accessToken = localSettings.Values["accessToken"] as string;
            var refreshToken = localSettings.Values["refreshToken"] as string;
            var role = localSettings.Values["role"] as string;


            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("No access token available");
            }

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("No refresh token available");
            }

            if (!JwtParser.IsTokenValid(refreshToken))
            {
                localSettings.Values["accessToken"] = "";
                localSettings.Values["refreshToken"] = "";

                if(role == "USER")
                {
                    _navigationService.NavigateTo("LoginForStudent");
                } else
                {
                    _navigationService.NavigateTo("LoginForTutor");

                }

                return CreateClient();
                
            }

            if (!JwtParser.IsTokenValid(accessToken))
            {
                
                accessToken = await _tokenService.refreshToken(_baseUrl);
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Unable to refresh token");
                }
            }

            return CreateClient(accessToken);
        }
    }
}
