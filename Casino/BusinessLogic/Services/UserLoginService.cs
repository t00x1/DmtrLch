using Domain.DTO;
using DataAccess.Repository;
using DataAccess.Wrapper;
using Domain.Interfaces.Repository;
using Domain.Common.Generic.Validation;
using Domain.Common.Generic;
using Domain.Interfaces.Common.Generic;
using Domain.Models;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Domain.Interfaces.Infrastructure.Email;
using Domain.Interfaces.BusinessLogic.Factory.Code;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Response;
using Domain.Interfaces.Infrastructure.File;
using Domain.Response;
using Domain.Interfaces.Infrastructure.JWT;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UserLoginService
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
        private readonly string _key;
       

        public UserLoginService(
            IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            IPropertyInfoExtractor propertyInfoExtractor,
            IBaseValidation baseValidation,
            IAutoMapper autoMapper,
            IServiceFactoryCode serviceFactoryCode,
            ILogger<UserRegisterService> logger,
            ISenderEmail senderEmail,
            ITextFileOperation textFileOperations,
            IJwtService jwtService,
            string key

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
            _key = key;

        }

        public async Task<IResponse<UserDTO>> Login(UserDTO entity)
        {
            if (entity == null)
            {
                _logger.LogWarning("Received null entity for registration.");
                return new Response<UserDTO>().InvalidInput("User data is null.");
            }

            if (new object?[] { entity.Password, entity.UserName }.Any(prop => prop == null))
            {
                _logger.LogWarning("One or more required fields are missing: Email, Username, Password, Name, or Surname.");
                return new Response<UserDTO>().InvalidInput("Required fields are missing.");
            }

            var clients = await _repositoryWrapper.User.FindByCondition(X => X.UserName == entity.UserName);


            var validClient = clients.FirstOrDefault(client => _passwordHasher.VerifyPassword(entity.Password, client.Password));
            if (validClient == null)
            {
                return new Response<UserDTO>().NotFound();
            }
            if(validClient.ConfirmedEmail == false)
            {

                return new Response<UserDTO>().Error("Email is not confirmed");
            }

            entity.Token = _jwtService.GenerateJwtToken(new UserDTO{IdOfUser = validClient.IdOfUser}, _key, DateTime.UtcNow.AddHours(168), null);

            return new Response<UserDTO>().Success(entity);
            
        }
    }
}