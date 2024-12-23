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
namespace BusinessLogic.Service
{
    public class UserRegisterService
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

        public UserRegisterService(
            IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            IPropertyInfoExtractor propertyInfoExtractor,
            IBaseValidation baseValidation,
            IAutoMapper autoMapper,
            IServiceFactoryCode serviceFactoryCode,
            ILogger<UserRegisterService> logger,
            ISenderEmail senderEmail,
            ITextFileOperation textFileOperations)
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
        }

        public async Task<IResponse<UserDTO>> Register(UserDTO entity)
        {
            if (entity == null)
            {
                _logger.LogWarning("Received null entity for registration.");
                return new Response<UserDTO>().InvalidInput("User data is null.");
            }

            if (new object?[] { entity.Email, entity.UserName, entity.Password, entity.Name, entity.Surname }
                .Any(prop => prop == null))
            {
                _logger.LogWarning("One or more required fields are missing: Email, Username, Password, Name, or Surname.");
                return new Response<UserDTO>().InvalidInput("Required fields are missing.");
            }

            _logger.LogInformation("Starting user registration for {UserName}", entity.UserName);

            if (!_baseValidation.Validate(entity))
            {
                _logger.LogWarning("Validation failed for user {UserName}.", entity.UserName);
                return new Response<UserDTO>().InvalidInput("Validation failed.");
            }

            var existingUsers = await _repositoryWrapper.User
                .FindByCondition(x => x.UserName == entity.UserName || (x.Email == entity.Email && x.ConfirmedEmail));

            var existUser = existingUsers.FirstOrDefault();
            if(existUser != null )
            {
                if(existUser.UserName == entity.UserName)
                {
                    _logger.LogWarning("User with Username {UserName} already exists.", entity.UserName);
                    return new Response<UserDTO>().Conflict("User with this username already exists.");
                }
                if (existUser.Email == entity.Email && existUser.ConfirmedEmail)
                {
                    _logger.LogWarning("User with Email {Email} already exists.", entity.Email);
                    return new Response<UserDTO>().Conflict("User with this email already exists.");
                }
            }

            User user = new User();
            _autoMapper.CopyPropertiesTo(entity, user);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.Password = _passwordHasher.GeneratePassword(entity.Password);
            user.IdOfUser = _serviceFactoryCode.Create("GUID").Generate();







           
            await _repositoryWrapper.Balance.Create(new Balance(){IdOfUser = user.IdOfUser, Balance1 = 0, UpdatedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow});
            await _repositoryWrapper.User.Create(user);
            await _repositoryWrapper.SaveChangesAsync();

            _logger.LogInformation("User {UserName} has been successfully registered.", entity.UserName);

            return new Response<UserDTO>().Success(entity);
        }
    }
}