
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI;
using TutorApp.Services.Interfaces.ForAPI;

namespace TutorApp.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserService(string baseUrl) {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<LoginResponse> LoginAccount(LoginRequest request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json,Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/auth/authenticate", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    // chuyển từ dạng json thành đối tượng
                    var responseData = JsonSerializer.Deserialize<JsonResponseLogin>(responseContent);
                    return new LoginResponse
                    {
                        Success = true,
                        Message = "Đăng ký thành công!",
                        Tokens = new JwtToken
                        {
                            accessToken = responseData.data.accessToken,
                            refreshToken = responseData.data.refreshToken
                        }
                    };
                }
                else
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = $"Đăng ký thất bại: {responseContent}"
                    };
                }


            }
            catch (Exception ex) {
                return new LoginResponse
                {
                    Message = $"Lỗi : {ex.Message}",
                    Success = false,
                };
            
            }
        }

        public async Task<RegisterResponse> RegisterAccount(RegisterRequest request)
        {
            try
            {

                // JsonSerializer.Serialize chuyển về dạng json 
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/user", content);

                //đọc kết quả trả về như là string
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new RegisterResponse
                    {
                        Success = true,
                        Message = "Đăng ký thành công!"
                    };
                }
                else
                {
                  
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = $"Đăng ký thất bại: {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = $"Lỗi kết nối: {ex.Message}"
                };
            }
        }
    }
}
