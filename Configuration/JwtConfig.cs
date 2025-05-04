using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GloboClima.Api.Configuration
{
    public static class JwtConfig
    {
        private static readonly string Secret = "nPL4L!yT$9xB#D6vq@FjZw3L^gQpR2Xk"; 
        public static string GenerateToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
                ValidateIssuer = false, // pode configurar se quiser usar Issuer
                ValidateAudience = false, // pode configurar se quiser usar Audience
                ClockSkew = TimeSpan.Zero // tolerância de expiração
            };
        }
    }
}
