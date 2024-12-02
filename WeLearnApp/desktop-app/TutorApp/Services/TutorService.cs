﻿using System;
using System.Collections.Generic;
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


    }
}
