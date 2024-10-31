using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Helpers
{
    public class JwtParser
    {   
        //lấy 1 giá trị trong object
        public static string GetClaim(string token, string claimType)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var claim = jwt.Claims.FirstOrDefault(c => c.Type == claimType);
                return claim?.Value;
            }
            catch
            {
                return null;
            }
        }

        // lấy giá trị của trường email trong object
        public static string GetEmail(string token)
        {
            return GetClaim(token, "email");
        }

        public static string GetRole(string token)
        {
            return GetClaim(token, "scope");
        }

        
        // lấy tất cả các giá trị của jwt object trả về
        public static Dictionary<string, string> GetAllClaims(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                return jwt.Claims.ToDictionary(
                    claim => claim.Type,
                    claim => claim.Value
                );
            }
            catch
            {
                return null;
            }
        }


        // lấy thời gian hết hạn
        public static DateTime? GetExpirationDate(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                return jwt.ValidTo.ToLocalTime();
            }
            catch
            {
                return null;
            }
        }

        // check xem token còn dùng được không 
        public static bool IsTokenValid(string token)
        {
            var expirationDate = GetExpirationDate(token);
            return expirationDate.HasValue && expirationDate.Value > DateTime.Now;
        }
    }
}