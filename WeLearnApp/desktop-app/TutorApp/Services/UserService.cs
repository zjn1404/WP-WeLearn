using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Request;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.JsonResponse;
using TutorApp.Services.Interfaces.ForAPI;
using System.Diagnostics;
using System.Net.Http;

namespace TutorApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpService _httpService;

        public UserService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LoginResponse> LoginAccount(LoginRequest request)
        {
            try
            {
                using (var client = _httpService.CreateClient())
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/auth/authenticate", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonResponseLogin>(responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return new LoginResponse
                        {
                            Success = true,
                            Code = responseData.code,
                            Message = "Login successfully",
                            Data = responseData.data
                        };
                    }
                    return new LoginResponse
                    {
                        Success = false,
                        Code = responseData.code,
                        Message = $"Login Failed: {responseData.message}",
                        Data = responseData.data
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Message = $"Error: {ex.Message}",
                    Success = false,
                };
            }
        }

        public async Task<RegisterResponse> RegisterAccount(RegisterRequest request)
        {
            try
            {
                using (var client = _httpService.CreateClient())
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/user", content);
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
                using (var client = _httpService.CreateClient())
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/auth/logout", content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return new LogoutResponse
                        {
                            Success = true,
                            Message = "Logout successfully!"
                        };
                    }
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
                using (var client = _httpService.CreateClient())
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/verification-code/verify", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonResponseVerify>(responseContent);

                    return new VerifyResponse
                    {
                        Success = response.IsSuccessStatusCode,
                        Code = responseData.code
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<ResendTokenResponse> ResendVerifyToken(string _id)
        {
            try
            {
                using (var client = _httpService.CreateClient())
                {
                    var content = new StringContent("application/json");
                    string url = $"/api/verification-code/{_id}";

                    var response = await client.PostAsync(url, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonResponseResendToken>(responseContent);

                    return new ResendTokenResponse
                    {
                        IsSuccess = response.IsSuccessStatusCode,
                        Code = responseData.code
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<UserProfileResponse> GetMyProfile(string token)
        {
            try
            {
                using (var client = _httpService.CreateClient(token))
                {

                    var response = await client.GetAsync("/api/user-profile/me");
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("responseContent", responseContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var userProfile = JsonSerializer.Deserialize<JsonResponseUserProfile>(responseContent);
                        return new UserProfileResponse
                        {
                            firstName = userProfile.data.firstName,
                            lastName = userProfile.data.lastName,
                            phoneNumber = userProfile.data.phoneNumber,
                            dob = userProfile.data.dob,
                            location = userProfile.data.location,
                            avatarUrl = userProfile.data.avatarUrl
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

        public async Task<UpdateProfileResponse> UpdateMyProfile(string token, UpdateProfileRequest request)
        {
            try
            {
                using (var client = _httpService.CreateClient(token))
                {
                    var json = JsonSerializer.Serialize(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync("/api/user-profile/me", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonResponseVerify>(responseContent);

                    Debug.WriteLine("UpdateMyProfile", responseContent);

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