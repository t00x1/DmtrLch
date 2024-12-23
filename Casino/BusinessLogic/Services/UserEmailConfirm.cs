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
using System.Linq;
using Domain.Interfaces.Infrastructure.JWT;

using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using DataAccess.Wrapper;

namespace BusinessLogic.Service
{
    public class UserEmailConfirm
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

        public UserEmailConfirm(
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
        public async Task<IResponse<UserDTO>> ConfirmCode(UserDTO entity)
        {
            if(entity == null ||
            string.IsNullOrEmpty(entity.Email) ||
             string.IsNullOrEmpty(entity.UserName) ||
            entity.Token == null ){ return new Response<UserDTO>().Error();}
            var codes = await _repositoryWrapper.Email.FindByCondition(X => X.Code == entity.Token && X.Expire > DateTime.UtcNow);
            if(!codes.Any()){
                return new Response<UserDTO>().NotFound();
            }
            EmailConfirmation Code =codes.FirstOrDefault();
            var users = await _repositoryWrapper.User.FindByCondition(X =>
                                                                    X.IdOfUser == Code.IdOfUser &&
                                                                    X.UserName== entity.UserName &&
                                                                    X.Email == entity.Email);
            var user = users.FirstOrDefault();
            if(user == null){

                return new Response<UserDTO>().NotFound();
            }
            user.ConfirmedEmail = true;
            _repositoryWrapper.User.Update(user);



             await _repositoryWrapper.SaveChangesAsync();
            return new Response<UserDTO>().Success(entity);


            

            


        }
    }
}