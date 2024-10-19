using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TutorApp.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUrl;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<ApiService> logger)
        {
            _clientFactory = clientFactory;
            _baseUrl = configuration["ApiBaseUrl"] ?? throw new ArgumentNullException("ApiBaseUrl is not configured");
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{_baseUrl}/{endpoint}");
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting data from {endpoint}: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{_baseUrl}/{endpoint}/{id}");
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting data by ID from {endpoint}: {ex.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync($"{_baseUrl}/{endpoint}", content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while posting data to {endpoint}: {ex.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, int id, object data)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync($"{_baseUrl}/{endpoint}/{id}", content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while putting data to {endpoint}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string endpoint, int id)
        {
            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.DeleteAsync($"{_baseUrl}/{endpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting data from {endpoint}: {ex.Message}");
                throw;
            }
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"API call failed with status code {response.StatusCode}: {errorContent}");
                throw new ApiException((int)response.StatusCode, errorContent);
            }
        }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

}

