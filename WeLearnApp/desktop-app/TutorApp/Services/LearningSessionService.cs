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
        private readonly HttpService _httpService;

        public LearningSessionService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LearningSessionResponse> CreateLearningSession(LearningSessionCreationRequest request)
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                if (httpClient == null)
                {
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(request, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/api/learning-session", content);

                return buildLearningSessionResponse(response).Result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task DeleteLearningSession(string id)
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                await httpClient.DeleteAsync(string.Format("/api/learning-session/%s", id));
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
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                var response = await httpClient.GetAsync(string.Format("/api/learning-session/%s", id));

                return buildLearningSessionResponse(response).Result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }



        public async Task<PageResponse<LearningSessionResponse>> GetLearningSessionList(int page, int size)
        {
            if (page < 0 || size <= 0)
                throw new ArgumentException("Invalid pagination parameters");

            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                var url = string.Format("/api/learning-session?page={0}&size={1}", page, size);
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {await response.Content.ReadAsStringAsync()}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);

                if (apiResponse?.data == null)
                {
                    throw new Exception("API response data is null");
                }

                var responseData = JsonSerializer.Deserialize<PageResponse<LearningSessionResponse>>(apiResponse.data.ToString());
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching learning sessions: {ex.Message}");
            }
        }


        public async Task<List<LearningSessionResponse>> GetMyLearningSessionList()
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                var response = await httpClient.GetAsync(string.Format("/api/learning-session/my-session"));
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                var learningSessions = JsonSerializer.Deserialize<List<LearningSessionResponse>>(responseData.data.ToString());

                return learningSessions;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<PageResponse<OrderResponse>> GetMyOrderedLearningSession(int page, int size)
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                var response = await httpClient.GetAsync(string.Format("/api/order/my-orders?page={0}&size={1}", page, size));
                var responseContent = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                var responseData = JsonSerializer.Deserialize<PageResponse<OrderResponse>>(apiResponse.data.ToString());

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
