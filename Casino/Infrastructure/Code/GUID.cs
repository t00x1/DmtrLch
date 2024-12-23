using Domain.Interfaces.Infrastructure.Code;
namespace Infrastructure.Code
{
    public class GuidUtility : ICode
    {
        public string Generate(int length = -1 )
        {
            return Guid.NewGuid().ToString();
        }
        public bool IsValid(string code)
        {
            return Guid.TryParse(code, out _);
        }
       
    }
}