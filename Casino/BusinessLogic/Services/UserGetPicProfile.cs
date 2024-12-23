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
    public class UserGetPicProfile
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
        private readonly IImageFileOperations _imageFileOperations;


        public UserGetPicProfile(
            IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            IPropertyInfoExtractor propertyInfoExtractor,
            IBaseValidation baseValidation,
            IAutoMapper autoMapper,
            IServiceFactoryCode serviceFactoryCode,
            ILogger<UserRegisterService> logger,
            ISenderEmail senderEmail,
            ITextFileOperation textFileOperations,
            IImageFileOperations imageFileOperations

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
            _imageFileOperations = imageFileOperations;

        }

        public async Task<IResponse<ImageDTO>> Get(string UserID, string Id)
        {
            IEnumerable<User> users;

            Console.WriteLine(UserID);
            users = await _repositoryWrapper.User.FindByCondition(X=> X.IdOfUser == Id || X.IdOfUser == UserID);
            if(!users.Any())
            {
                return new Response<ImageDTO>().NotFound("User not found");
            }
            var Userr =  UserID != "null" && !string.IsNullOrEmpty(UserID) ? users.FirstOrDefault(X => X.IdOfUser == UserID) : users.FirstOrDefault(X => X.IdOfUser == Id);
            string directoryPath = @"../../Images";
            var List = await _repositoryWrapper.Image.FindByCondition(X=> X.IdOfImage == Userr.Avatar);
            var avatar = List.FirstOrDefault();
            if(avatar == null)
            {
                Console.WriteLine("__________________");
                
                return new Response<ImageDTO>().Success(new ImageDTO());
            }
            string fullname = avatar.IdOfImage + avatar.Directory;
            Stream stream = _imageFileOperations.ReadImage( Path.Combine(directoryPath,fullname));

           
            return new Response<ImageDTO>().Success(new ImageDTO(){Stream = stream, Name = fullname});
        }
       
    }
}