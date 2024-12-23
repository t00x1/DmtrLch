using Domain.Interfaces.Infrastructure.Code;
using Infrastructure.Code;
using Microsoft.Extensions.DependencyInjection;
using  Domain.Interfaces.BusinessLogic.Factory.Code;
namespace BusinessLogic.Factory
{
    public class ServiceFactoryCode : IServiceFactoryCode
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceFactoryCode(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

   public ICode Create(string type)
    {
        return type switch
        {
            "GUID" => _serviceProvider.GetRequiredService<GuidUtility>(),  // Разрешаем конкретный тип
            "USCODE" => _serviceProvider.GetRequiredService<UsCode>(),    // Разрешаем другой тип
            _ => throw new ArgumentException("Invalid type", nameof(type)),
        };
    }

}

}
