﻿using Newtonsoft.Json.Linq;
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
using Windows.Web.Http;
using HttpMethod = System.Net.Http.HttpMethod;
using HttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace TutorApp.Services
{
    public class TutorService : ITutorService
    {

        private readonly HttpService _httpService;

        public TutorService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<TutorDetail> GetDetailTutorService(string id)
        {
            try
            {
                using (var httpClient = await _httpService.AuthenticatedCallAPI())
                {
                    if (httpClient == null)
                    {
                        return null;
                    }
                    var url = $"/api/tutor/{id}";

                    if (httpClient == null) return null;
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

        public async Task<PageResponse<Tutor>> getListTutor(int page, int size)
        {
            try
            {
                using (var httpClient = await _httpService.AuthenticatedCallAPI())
                {
                    if (httpClient == null)
                    {
                        return null;
                    }
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

        public async Task<PageResponse<Tutor>> GetListTutorByFilters(int page, int size, FilterTutor filters)
        {
            try
            {
                using (var httpClient = await _httpService.AuthenticatedCallAPI())
                {
                    if (httpClient == null)
                    {
                        return null;
                    }
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

        public async Task<PageResponse<Tutor>> GetListTutorBySearch(int page, int size, string name)
        {
            try
            {
                using (var httpClient = await _httpService.AuthenticatedCallAPI())
                {
                    if (httpClient == null)
                    {
                        return null;
                    }
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


        public async Task<TutorSpecificFieldsResponse> GetTutorSpecificFields( string id)
        {
            try
            {
                using (var client = await _httpService.AuthenticatedCallAPI())
                {
                    if (client == null)
                    {
                        return null;
                    }

                    var response = await client.GetAsync($"/api/tutor/{id}");
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
                    return new TutorSpecificFieldsResponse
                    {
                        Degree = "",
                        Description = "",

                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<UpdateProfileResponse> UpdateTutorSpecificFields(UpdateTutorSpecificFieldsRequest request)
        {
            try
            {
                using (var client = await _httpService.AuthenticatedCallAPI())
                {
                    if (client == null)
                    {
                        return null;
                    }
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
