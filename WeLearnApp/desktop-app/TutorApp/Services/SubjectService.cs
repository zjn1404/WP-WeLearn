using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TutorApp.Models;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpService _httpService;

        public SubjectService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Subject>> GetSubjects()
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var accessToken = localSettings.Values["accessToken"]?.ToString();

                using (var httpClient = _httpService.CreateClient(accessToken))
                {
                    
                    var url = $"/api/subject/all";

                    var request = new HttpRequestMessage(HttpMethod.Get, url);

                    var response = await httpClient.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var subjectListResponses = JsonSerializer.Deserialize<List<Subject>>(responseData.data.ToString());
                        return subjectListResponses;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
