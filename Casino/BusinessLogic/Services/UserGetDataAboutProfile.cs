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

namespace BusinessLogic.Service
{
    public class UserGetDataAboutProfile
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

        public UserGetDataAboutProfile(
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
        public async Task<IResponse<User>> Get(string UserID, string Id) // один типа свой который в токене другой получаемый
        {
            IEnumerable<User> users;

            Console.WriteLine(UserID);
            
            users = await _repositoryWrapper.User.FindByCondition(X=> X.IdOfUser == Id || X.IdOfUser == UserID);
            
            

 
            if (!users.Any())
            {
                return new Response<User>().Error("User not found.");
            }
            var result = UserID != "null" && !string.IsNullOrEmpty(UserID) ? users.FirstOrDefault(X => X.IdOfUser == UserID) : users.FirstOrDefault(X => X.IdOfUser == Id);
            result.Password = "";
            return new Response<User>().Success(result);
        }

    }
}