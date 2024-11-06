using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Helpers
{
    /// <summary>
    /// A helper class for parsing and extracting information from JWT (JSON Web Token) tokens.
    /// Provides methods to extract claims, check token validity, and retrieve expiration information.
    /// </summary>
    public class JwtParser
    {
        /// <summary>
        /// Retrieves the value of a specific claim from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <param name="claimType">The type of claim to retrieve (e.g., "email", "scope").</param>
        /// <returns>The value of the specified claim, or null if the claim is not found or the token is invalid.</returns>
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

        /// <summary>
        /// Retrieves the email address from the JWT token.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <returns>The email address claim, or null if the claim is not found or the token is invalid.</returns>
        public static string GetEmail(string token)
        {
            return GetClaim(token, "email");
        }


        /// <summary>
        /// Retrieves the role from the JWT token.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <returns>The role claim (scope), or null if the claim is not found or the token is invalid.</returns>
        public static string GetRole(string token)
        {
            return GetClaim(token, "scope");
        }


        /// <summary>
        /// Retrieves all claims from the JWT token as a dictionary.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <returns>A dictionary containing all claims with claim type as keys and claim values as values, or null if the token is invalid.</returns>
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


        /// <summary>
        /// Retrieves the expiration date of the JWT token.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <returns>The expiration date as a DateTime, or null if the token is invalid.</returns>
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

          /// <summary>
          /// Checks if the JWT token is still valid based on its expiration date.
          /// </summary>
          /// <param name="token">The JWT token as a string.</param>
          /// <returns>True if the token is valid (not expired), otherwise false.</returns> 
        public static bool IsTokenValid(string token)
        {
            var expirationDate = GetExpirationDate(token);
            return expirationDate.HasValue && expirationDate.Value > DateTime.Now;
        }
    }
}