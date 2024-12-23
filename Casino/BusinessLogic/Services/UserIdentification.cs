using Domain.DTO;
using DataAccess.Repository;
using DataAccess.Wrapper;
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
    public class UseridentificationService
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

        public UseridentificationService(
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

       public async Task<string> Authorize(string token)
        {
            var result = _jwtService.DecodeJwtToken(token, _key);
            if (!result.IsValid)
            {
                return "Invalid token";
            }


            var userId = result.Claims.FirstOrDefault(c => c.Key == "IdOfUser").Value;

            if (userId != null)
            {
                var users = await _repositoryWrapper.User.FindByCondition(X => X.IdOfUser == userId);
                if(users.Any()){

                    return userId;  
                }
            }

            return ""; 
        }

    }
}
