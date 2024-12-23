using Domain.Interfaces.Repository;
using Domain.Common.Generic.Validation;
using Domain.Common.Generic;
using System.Security.Claims;

using Domain.Interfaces.Common.Generic;
using Domain.Models;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Infrastructure.Email;
using Domain.Interfaces.BusinessLogic.Factory.Code;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Response;
using Domain.Interfaces.Infrastructure.File;
using Domain.Response;
using Domain.Interfaces.Infrastructure.JWT;

using System.Linq;
using System.Threading.Tasks;
using DataAccess.Wrapper;
using Domain.DTO;

namespace BusinessLogic.Service
{
    public class CuponActivate
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IPropertyInfoExtractor _propertyInfoExtractor;
        private readonly IBaseValidation _baseValidation;
        private readonly IAutoMapper _autoMapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IServiceFactoryCode _serviceFactoryCode;
        private readonly ILogger<UserRegisterService> _logger;
        private readonly ISenderEmail _senderEmail;
        private readonly ITextFileOperation _textFileOperations;
        private readonly IJwtService _jwtService;

        public CuponActivate(
            IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            IPropertyInfoExtractor propertyInfoExtractor,
            IBaseValidation baseValidation,
            IAutoMapper autoMapper,
            IServiceFactoryCode serviceFactoryCode,
            ILogger<UserRegisterService> logger,
            ISenderEmail senderEmail,
            ITextFileOperation textFileOperations,
            IJwtService jwtService
        )
        {
            _passwordHasher = passwordHasher;
            _repositoryWrapper = repositoryWrapper;
            _propertyInfoExtractor = propertyInfoExtractor;
            _baseValidation = baseValidation;
            _autoMapper = autoMapper;
            _serviceFactoryCode = serviceFactoryCode;
            _logger = logger;
            _senderEmail = senderEmail;
            _textFileOperations = textFileOperations;
            _jwtService = jwtService;
        }
        public async Task<IResponse<UserDTO>> Activate(string IdOfUser, string IdOfCupon)
        {
            var Cupons = await _repositoryWrapper.Cupons.FindByCondition(X => X.IdOfCupon == IdOfCupon);
            var Cupon = Cupons.FirstOrDefault();
            
            if (Cupon != null)
            {
                if (!Cupon.Reusable)
                {
                    var used = await _repositoryWrapper.CuponsUsed.FindByCondition(X => X.IdOfCupon == IdOfCupon && X.IdOfUser == IdOfUser);
                    if (used.Any())
                    {
                        return new Response<UserDTO>().Error("Уже использован");
                    }
                }

                var balances = await _repositoryWrapper.Balance.FindByCondition(X => X.IdOfUser == IdOfUser);
                var balance = balances.FirstOrDefault(); 
                
                if (balance != null)
                {
                    balance.Balance1 += Cupon.Value;
                    await _repositoryWrapper.CuponsUsed.Create(new CuponsUsed(){IdOfCupon = IdOfCupon, IdOfUser = IdOfUser, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow});
                    await _repositoryWrapper.Balance.Update(balance);
                    await _repositoryWrapper.SaveChangesAsync();
                    return new Response<UserDTO>().Success(new UserDTO());  // Заполнить объект 
                }
                Console.WriteLine(IdOfUser);
                return new Response<UserDTO>().Error("Баланс не найден");
                
            }
            
            return new Response<UserDTO>().NotFound();
        }


    }
}