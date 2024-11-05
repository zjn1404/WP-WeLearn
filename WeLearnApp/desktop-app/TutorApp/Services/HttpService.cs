using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Services
{
    public class HttpService
    {
       
            private readonly string _baseUrl;

            public HttpService(string baseUrl)
            {
                _baseUrl = baseUrl;
            }

            public HttpClient CreateClient(string? token = null)
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = false, // Tắt credentials mặc định
                    AutomaticDecompression = DecompressionMethods.None, // Tắt nén tự động
                };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(_baseUrl),
                    DefaultRequestHeaders =
            {
                Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
            }
                };

                // Clear tất cả header mặc định
                client.DefaultRequestHeaders.Clear();

                // Chỉ thêm token nếu có
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                return client;
            }
        
    }
}
