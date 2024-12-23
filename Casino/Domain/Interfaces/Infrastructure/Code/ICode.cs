namespace Domain.Interfaces.Infrastructure.Code
{
    public interface ICode
    {
       public string Generate(int length = 6 );
        public bool IsValid(string code);
    }
}