//using System.IdentityModel.Tokens.Jwt;

//namespace EmployeeAdmin.Helpers
//{
//    public class TokenHelper
//    {
//        public static bool IsTokenExpired(string token)
//        {
//            var handler = new JwtSecurityTokenHandler();
//            if (handler.CanReadToken(token))
//            {
//                var jwtToken = handler.ReadJwtToken(token);
//                var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

//                if (expClaim != null)
//                {
//                    var tokenExpirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value)).UtcDateTime;
//                    return tokenExpirationTime < DateTime.UtcNow;
//                }
//            }

//            return true;
//        }
//    }

//}
//using Azure.Core;
//using Microsoft.AspNetCore.Mvc;

//private IActionResult ValidateToken()
//{
//    var authHeader = Request.Headers["Authorization"].ToString();
//    if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
//    {
//        return Unauthorized(new { error = "Authorization header is missing or invalid" });
//    }

//    // Extract the token (remove "Bearer " prefix)
//    var token = authHeader.Substring("Bearer ".Length);

//    // Check if the token is expired
//    if (TokenHelper.IsTokenExpired(token))
//    {
//        return Unauthorized(new { error = "Token has expired" });
//    }

//    return null;

//}