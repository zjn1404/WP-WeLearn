using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Services.Interfaces.ForAPI;
using System.Text.Json;

namespace TutorApp.Services
{
    public class VideoCallService : IVideoCallService
    {
        private readonly HttpService _httpService;

        public VideoCallService(HttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<string> JoinRoom(string learningSessionId)
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();
                if (httpClient == null)
                {
                    return null;
                }

                var url = $"/api/session-room/{Uri.EscapeDataString(learningSessionId)}";

                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                if (response.IsSuccessStatusCode)
                {
                    string rooomUrl = responseData.data.ToString();
                    return rooomUrl;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Joining room: {ex.Message}");
                throw new Exception("Error while joining room", ex);
            }
        }
    }
}
