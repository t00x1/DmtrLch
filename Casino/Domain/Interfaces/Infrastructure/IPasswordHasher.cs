namespace Domain.Interfaces.Infrastructure
{
    public interface IPasswordHasher
    {
        string? GeneratePassword(string password, int workFactor = 12);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
