
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.JsonResponse;
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
                var responseData = JsonSerializer.Deserialize<JsonResponseLogin>(responseContent);

                if (response.IsSuccessStatusCode)
                {

                    // chuyển từ dạng json thành đối tượng
                    return new LoginResponse
                    {
                        Success = true,
                        Code = responseData.code,
                        Message = "Login successfully",
                        Data = responseData.data
                    };
                } 
                else
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Code = responseData.code,
                        Message = $"Login Failed: {responseData.message}",
                        Data = responseData.data
                    };
                }


            }
            catch (Exception ex) {
                return new LoginResponse
                {
                    Message = $"Error : {ex.Message}",
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
                var responseData = JsonSerializer.Deserialize<JsonResponseRegister>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    return new RegisterResponse
                    {
                        Success = true,
                        Message = "Register successfully",
                        Code = responseData.code,
                        Data = new JsonResponseForDataRegister
                        {
                            id = responseData.data.id,
                            email = responseData.data.email,
                            role = responseData.data.role,
                            username = responseData.data.username
                        }
                    };
                }
                else
                {
                  
                    return new RegisterResponse
                    {
                        Success = false,
                        Message = $"Register Failed: {responseContent}",
                        Code = responseData.code,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<LogoutResponse> LogoutAccount(LogoutRequest request)
        {
            try
            {

                // JsonSerializer.Serialize chuyển về dạng json 
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/auth/logout", content);

                //đọc kết quả trả về như là string
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new LogoutResponse
                    {
                        Success = true,
                        Message = "Logout successfully!"
                    };
                }
                else
                {

                    return new LogoutResponse
                    {
                        Success = false,
                        Message = $"Logout failed: {responseContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new LogoutResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<VerifyResponse> VerifyAccount(VerifyRequest request)
        {
            try
            {
                // JsonSerializer.Serialize chuyển về dạng json 
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/verification-code/verify", content);

                //đọc kết quả trả về như là string
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<JsonResponseVerify>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    return new VerifyResponse
                    {
                        Success = true,
                        Code = responseData.code
                    };
                }
                else
                {

                    return new VerifyResponse
                    {
                        Success = false,
                        Code = responseData.code
                    };
                }
            }
            catch (Exception ex) {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<ResendTokenResponse> ResendVerifyToken(string _id)
        {
            try
            {
              
                var content = new StringContent("application/json");
                string url = "/api/verification-code/" + _id;

                var response = await _httpClient.PostAsync(url, content);

                //đọc kết quả trả về như là string
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<JsonResponseResendToken>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    return new ResendTokenResponse
                    {
                        IsSuccess = true,
                        Code = responseData.code
                    };
                }
                else
                {

                    return new ResendTokenResponse
                    {
                        IsSuccess = false,
                        Code = responseData.code
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
    }
}
