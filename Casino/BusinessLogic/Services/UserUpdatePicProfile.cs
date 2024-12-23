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
    public class UserUpdatePicProfile
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

        public UserUpdatePicProfile(
            IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            IPropertyInfoExtractor propertyInfoExtractor,
            IBaseValidation baseValidation,
            IAutoMapper autoMapper,
            IServiceFactoryCode serviceFactoryCode,
            ILogger<UserRegisterService> logger,
            ISenderEmail senderEmail,
            ITextFileOperation textFileOperations,
            IImageFileOperations imageFileOperations)
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

        public async Task<IResponse<ImageDTO>> Update( ImageDTO image, string id)
        {
            if (image == null || string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Received null entity.");
                return new Response<ImageDTO>().InvalidInput("User data is null.");
            }
            var Users = await _repositoryWrapper.User.FindByCondition(X=> X.IdOfUser == id);
            var Userr = Users.FirstOrDefault();
            string fileName = _serviceFactoryCode.Create("GUID").Generate();
            string directoryPath = @"../../Images";
            Userr.Avatar = fileName;
            _imageFileOperations.WriteImage(directoryPath,fileName + Path.GetExtension(image.Name) , image.Stream);
            await _repositoryWrapper.Image.Create( new Image(){IdOfImage = fileName, Directory = Path.GetExtension(image.Name)});
            await _repositoryWrapper.User.Update(Userr);
            await _repositoryWrapper.SaveChangesAsync();

           
            return new Response<ImageDTO>().Success(new ImageDTO());
        }
    }
}