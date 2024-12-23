using Domain.Interfaces.Infrastructure.Code;
namespace Infrastructure.Code
{
    public class UsCode : ICode
    {
        public string Generate(int length = 6 )
        {
            Random r = new Random();   
            int number = r.Next(100000, 1000000);
            return number.ToString();
        }
         public bool IsValid(string code){ // а че? потом 
            return true;
         }
    }
}