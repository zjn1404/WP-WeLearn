using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class TutorService : ITutorService
    {

        private readonly HttpService _httpService;

        public TutorService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<TutorDetail> GetDetailTutorService(string id, string token)
        {
            try
            {
                using (var httpClient = _httpService.CreateClient(token))
                {
                    var url = $"/api/tutor/{id}";

                    var response = await httpClient.GetAsync(url);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                    if (response.IsSuccessStatusCode)
                    {
                        var data = JsonSerializer.Deserialize<TutorDetail>(responseData.data.ToString());
                        return data;
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

        public async Task<PageResponse<Tutor>> getListTutor(int page, int size,string token)
        {
            try
            {
                using (var httpClient = _httpService.CreateClient(token))
                {
                    var url = $"/api/user-profile/filter?page={page}&size={size}";

                    var body = new StringContent("{}", Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Get, url)
                    {
                        Content = body
                    };

                    var response = await httpClient.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                   

                    if (response.IsSuccessStatusCode)
                    {
                        var pageResponse = JsonSerializer.Deserialize<PageResponse<Tutor>>(responseData.data.ToString());
                        return pageResponse;
                    } else
                    { 
                       return null;
                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<PageResponse<Tutor>> GetListTutorByFilters(int page, int size, FilterTutor filters, string token)
        {
            try
            {
                using (var httpClient = _httpService.CreateClient(token))
                {
                    var url = $"/api/user-profile/filter?page={page}&size={size}";


               
                    var dataFromRequest = JsonSerializer.Serialize(filters);

         
                    var body = new StringContent(dataFromRequest, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Get, url)
                    {
                        Content = body
                    };

                    var response = await httpClient.SendAsync(request);


                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                    if (response.IsSuccessStatusCode)
                    {
                        var pageResponse = JsonSerializer.Deserialize<PageResponse<Tutor>>(responseData.data.ToString());
                        return pageResponse;
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

        public async Task<PageResponse<Tutor>> GetListTutorBySearch(int page, int size, string name, string token)
        {
            try
            {
                using (var httpClient = _httpService.CreateClient(token))
                {
                    var url = $"/api/user-profile/search?page={page}&size={size}&keyword={name}";


                    var body = new StringContent("{}", Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Get, url)
                    {
                        Content = body
                    };

                    var response = await httpClient.SendAsync(request);


                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                    if (response.IsSuccessStatusCode)
                    {
                        var pageResponse = JsonSerializer.Deserialize<PageResponse<Tutor>>(responseData.data.ToString());
                        return pageResponse;
                    }
                    else
                    {
                        return null;
                    }
                
            }
            }
            catch (Exception ex) {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public Task<Tutor> getTutorInfo(string id)
        {
            throw new NotImplementedException();
        }


        public async Task<TutorSpecificFieldsResponse> GetTutorSpecificFields(string token, string id)
        {
            try
            {
                using (var client = _httpService.CreateClient(token))
                {

                    var response = await client.GetAsync($"/api/tutor/${id}");
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("responseContent", responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var tutorSpecificFields = JsonSerializer.Deserialize<JsonResponseTutorSpecificFields>(responseContent);
                        return new TutorSpecificFieldsResponse
                        {
                            Degree = tutorSpecificFields.data.degree,
                            Description = tutorSpecificFields.data.description,

                        };
                    }
                    throw new Exception($"Failed to retrieve profile: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<UpdateProfileResponse> UpdateTutorSpecificFields(string token, UpdateTutorSpecificFieldsRequest request)
        {
            try
            {
                using (var client = _httpService.CreateClient(token))
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync("/api/tutor", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonResponseVerify>(responseContent);

                    Debug.WriteLine("Update Tutor Profile", responseContent);

                    return new UpdateProfileResponse
                    {
                        Success = response.IsSuccessStatusCode,
                        Message = response.IsSuccessStatusCode ?
                            "Update successfully!" :
                            $"Update failed: {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
