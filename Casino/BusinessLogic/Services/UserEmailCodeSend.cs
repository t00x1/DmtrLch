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

namespace BusinessLogic.Service
{
    public class UserEmailCodeSend
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

        public UserEmailCodeSend(
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
        public async Task<IResponse<UserDTO>> SendCode(UserDTO entity)
        {
            if(entity == null || string.IsNullOrEmpty(entity.Email) ){ return new Response<UserDTO>().Error();}
            string code = _serviceFactoryCode.Create("USCODE").Generate();
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(5);
            string emailText = code;
            // "<!doctype html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Код подтверждения регистрации</title><style>body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f8f9fa; display: flex; justify-content: center; align-items: center; height: 100vh; } .container { border: 1px solid #ced4da; border-radius: 5px; padding: 20px; background-color: #ffffff; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); width: 300px; text-align: center; } img { width: 100px; height: auto; margin-bottom: 15px; } h1 { color: #343a40; font-size: 24px; margin-bottom: 10px; } p { color: #495057; font-size: 16px"


            
            // _textFileOperations.ReadText("../Domain/Letter.html")
            

                // .Replace("{Code}", code)
                // .Replace("{Valid}", expirationTime.ToString("HH:mm"));

                if(!_senderEmail.SendEmail(entity.Email ?? "Example@gmail.com", "Код подтверждения", emailText)) // вернуть html body
                {
                    return new Response<UserDTO>().Error();
                };
            var Users = await _repositoryWrapper.User.FindByCondition(X => X.Email == entity.Email);
            foreach(var user in Users) // временно 
            {

                EmailConfirmation emailConfirmation = new EmailConfirmation
                {
                    IdOfUser = user.IdOfUser,
                    IdReq = _serviceFactoryCode.Create("GUID").Generate(),
                    Expire = expirationTime,
                    Code = code,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _repositoryWrapper.Email.Create(emailConfirmation);
            }
            await _repositoryWrapper.SaveChangesAsync();
            return new Response<UserDTO>().Success(entity);
        }
    }
}