using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Domain.DTO;
using System.Text;
using System.Reflection;
using Domain.Interfaces.Infrastructure.JWT;
namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        public string? GenerateJwtToken<T>(T entity, string secretKey, DateTime expirationTime, string? tokenId = null) 
        {
            var claims = new List<Claim>();
           PropertyInfo[] props = typeof(T).GetProperties();
           if(entity == null){return null;}
           foreach(var el in props)
           {
                if(el == null){continue;}
                object value = el.GetValue(entity);
                if(value is string s && value != null)
                {

                    claims.Add(new Claim(el.Name,s));
                }
           }
            if (!string.IsNullOrEmpty(tokenId))
            {
                claims.Add(new Claim("TokenID", tokenId));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public TokenDecodeResult DecodeJwtToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                
                var claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value);

                return new TokenDecodeResult
                {
                    Claims = claims,
                    IsValid = true
                };
            }
            catch (SecurityTokenException ex)
            {
                return new TokenDecodeResult
                {
                    Claims = null,
                    IsValid = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
