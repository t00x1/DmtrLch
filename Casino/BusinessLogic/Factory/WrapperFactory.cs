// using Domain.Interfaces.Infrastructure.Code;
// using Infrastructure.Code;
// using Microsoft.Extensions.DependencyInjection;
// using  Domain.Interfaces.BusinessLogic.Factory.Code;
// using Domain.Interfaces.Repository;
// using CasinoApi.Containers;
// using DataAccess.Wrapper;
// namespace BusinessLogic.Factory
// {
  
// public class WrapperFactory : IWrapperFactory
// {
//     private readonly IServiceScopeFactory _serviceScopeFactory;

//     public WrapperFactory(IServiceScopeFactory serviceScopeFactory)
//     {
//         _serviceScopeFactory = serviceScopeFactory;
//     }

//     public IRepositoryWrapper Create(WrappersEnums type)
//     {
//         using (var scope = _serviceScopeFactory.CreateScope())
//         {
//             var serviceProvider = scope.ServiceProvider;

//             return type switch
//             {
//                 WrappersEnums.SingleTone => serviceProvider.GetRequiredService<RepositoryWrapperServer>(),
//                 WrappersEnums.Basic => serviceProvider.GetRequiredService<RepositoryWrapper>(),
//                 _ => throw new ArgumentException("Invalid type", nameof(type)),
//             };
//         }
//     }
// }

// }
