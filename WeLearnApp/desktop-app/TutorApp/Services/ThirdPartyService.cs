
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
using System.Diagnostics;
using Windows.Media.Protection.PlayReady;

namespace TutorApp.Services
{
    public class ThirdPartyService : IThirdPartyService
    {
        private static readonly HttpClient client = new();


        public async Task<List<Province>> GetProvinceList()
        {
            try
            {
                string url = $"https://provinces.open-api.vn/api/?depth=1";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Province> provinceList = JsonSerializer.Deserialize<List<Province>>(json);

                    return provinceList;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data. Status Code: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<District>> GetDistrictList()
        {
            try
            {
                string url = $"https://provinces.open-api.vn/api/d/?depth=1";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<District> districtList = JsonSerializer.Deserialize<List<District>>(json);

                    return districtList;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data. Status Code: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<District>> GetDistrictList(int provinceCode)
        {
            try
            {
                List<District> districtList = new List<District>();
                string url = $"https://provinces.open-api.vn/api/p/{provinceCode}?depth=2";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Province provinceResponse = JsonSerializer.Deserialize<Province>(json);

                    Console.WriteLine($"Districts in {provinceResponse.name}:");
                    foreach (var district in provinceResponse.districts)
                    {
                        districtList.Add(district);    
                    }

                    return districtList;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data. Status Code: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }

}
