using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace LoginAndRegistrationWebAPI.Access
{
    internal static partial class AccessControl
    {
        internal static string GetToken(string login)
        {
            var credentials = new SigningCredentials
                (TokenOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = DateTime.UtcNow.AddDays(TokenOptions.LIFETIME),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }


        private static class TokenOptions
        {
            public const int LIFETIME   = 7;
            private const string KEY    = "LoginAndRegistrationWebAPITestTaskSecurityKey";

            public static SymmetricSecurityKey GetSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
        }
    }
}
