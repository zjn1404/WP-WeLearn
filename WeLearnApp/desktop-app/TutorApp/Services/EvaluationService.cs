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
    public class EvaluationService : IEvaluationService
    {
        private readonly HttpService _httpService;

        public EvaluationService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<EvaluationResponse> evaluate(EvaluationRequest request, string token)
        {
            if (request.star <= 0)
            {
                throw new Exception("Star must be greater than 0");
            }
            try
            {
                using (var client = _httpService.CreateClient(token))
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/evaluate", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                    var evaluation = JsonSerializer.Deserialize<EvaluationResponse>(responseData.data.ToString());

                    return new EvaluationResponse
                    {
                        comment = evaluation.comment,
                        star = evaluation.star,
                        tutorId = evaluation.tutorId
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<PageResponse<EvaluationResponse>> getAllEvaluation(string tutorId, int page, int size, string token)
        {
            try
            {
                using (var httpClient = _httpService.CreateClient(token))
                {
                    var url = $"/api/evaluate/tutor?tutorId={tutorId}&page={page}&size={size}";

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
                        var pageResponse = JsonSerializer.Deserialize<PageResponse<EvaluationResponse>>(responseData.data.ToString());
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
    }
}
