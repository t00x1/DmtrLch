using Domain.DTO;
namespace Domain.Interfaces.Infrastructure.JWT
{
    public interface IJwtService
    {
        string? GenerateJwtToken<T>(T entity, string secretKey, DateTime expirationTime, string? tokenId = null);

        TokenDecodeResult DecodeJwtToken(string token, string secretKey);
    }
}