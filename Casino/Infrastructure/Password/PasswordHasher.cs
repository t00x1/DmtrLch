using Domain.Interfaces.Infrastructure;
namespace Infrastructure.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        
       public string? GeneratePassword(string password, int workFactor = 12)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

public bool VerifyPassword(string password, string hashedPassword)
{
    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
    {
        throw new ArgumentNullException(nameof(password), "Password and hashed password cannot be null or empty.");
    }
    bool result;
    try{
        result = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }catch( Exception ex)
    {
        return false;   
    }
    return result;
}

    }
}
