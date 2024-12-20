using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Services.Interfaces.ForAPI;
using System.Net.Http;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Diagnostics;

namespace TutorApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpService _httpService;

        public PaymentService(HttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<string> CreatePayment(string amount, string learningSessionId)
        {
            try
            {
                using var httpClient = await _httpService.AuthenticatedCallAPI();

                var url = $"/api/payment/create-payment?amount={Uri.EscapeDataString(amount)}&learningSessionId={Uri.EscapeDataString(learningSessionId)}";

                var response = await httpClient.GetAsync(url);
                
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var responseData = JsonSerializer.Deserialize<ApiResponse>(responseContent);


                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<PaymentResponse>(responseData.data.ToString());
                    return data.paymentUrl.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in CreatePayment: {ex.Message}");
                throw new Exception("Error while creating payment", ex);
            }
        }
    }

}
