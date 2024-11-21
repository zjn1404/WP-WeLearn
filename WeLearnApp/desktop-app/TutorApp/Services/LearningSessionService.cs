using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Services.Interfaces.ForAPI;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace TutorApp.Services
{

    public class LearningSessionService : ILearningSessionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        
        public LearningSessionService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<LearningSessionResponse> CreateLearningSession(LearningSessionCreationRequest request)
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var token = localSettings.Values["accessToken"] as string;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine(request);
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(request, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/learning-session", content);

                return buildLearningSessionResponse(response).Result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
        
        public async Task DeleteLearningSession( string id)
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var token = localSettings.Values["accessToken"] as string;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await _httpClient.DeleteAsync(string.Format("/api/learning-session/%s", id));
            }
            catch (Exception ex) 
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<LearningSessionResponse> GetLearningSession(string id)
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var token = localSettings.Values["accessToken"] as string;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(string.Format("/api/learning-session/%s", id));
                
                return buildLearningSessionResponse(response).Result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<PageResponse<LearningSessionResponse>> GetLearningSessionList(int page, int size)
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var token = localSettings.Values["accessToken"] as string;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(string.Format("/api/learning-session?page={0}&size={1}", page, size));
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = (PageResponse<LearningSessionResponse>)JsonSerializer.Deserialize<ApiResponse>(responseContent).data;

                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        private async Task<LearningSessionResponse> buildLearningSessionResponse(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseData = JsonSerializer.Deserialize<JsonResponseForLearningSession>(responseContent, options);

            if (responseData?.Data != null)
            {
                var learningSession = new LearningSessionResponse
                {
                    Id = responseData.Data.Id,
                    Duration = responseData.Data.Duration,
                    Grade = responseData.Data.Grade,
                    Subject = responseData.Data.Subject,
                    LearningMethod = responseData.Data.LearningMethod,
                    Tuition = responseData.Data.Tuition,
                    StartTime = responseData.Data.StartTime,
                };
                return learningSession;
            }
            else
            {
                // Handle the case where responseData.Data is null
                throw new Exception("Invalid response data");
            }
        }
    }

}
