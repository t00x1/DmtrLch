using Domain.Interfaces.Infrastructure.Code;
namespace Domain.Interfaces.BusinessLogic.Factory.Code
{
    public interface IServiceFactoryCode
    {
        ICode Create(string type);
    }
}
