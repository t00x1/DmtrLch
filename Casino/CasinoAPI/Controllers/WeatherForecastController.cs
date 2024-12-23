using Microsoft.AspNetCore.Mvc;
using Domain.DTO;
using BusinessLogic.Service;
using Domain.Interfaces.Response;
using Casino.Handlers; 
using System.Threading.Tasks;
using Domain.Models;
using BusinessLogic.Service;
namespace Casino.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Client : ControllerBase
    {
        private readonly UserRegisterService _userRegisterService;
        private readonly UserLoginService _userLoginService;
        private readonly UserEmailCodeSend _userEmailCodeSend;
        
        private readonly UserGetDataAboutProfile _userGetData;
        private readonly UserEmailConfirm  _userEmailConfirm;
        private readonly UserUpdatePicProfile  _userUpdateDataProfile;
        private readonly UserGetPicProfile  _userGetPicProfile;
        private readonly CuponActivate  _cuponActivate;
        private readonly BalanceGet  _balanceGet;
        


 
        public Client(UserUpdatePicProfile userUpdateDataProfile,BalanceGet balanceGet,CuponActivate cuponActivate ,UserRegisterService userRegisterService,UserLoginService userLoginService, UserGetDataAboutProfile userGetData,UserEmailCodeSend userEmailCodeSend, UserEmailConfirm userEmailConfirm, UserGetPicProfile userGetPicProfile)
        {
            _userRegisterService = userRegisterService;
            _userLoginService = userLoginService;
            _userGetData = userGetData;
            _userEmailCodeSend = userEmailCodeSend;
            _userEmailConfirm = userEmailConfirm;
            _userUpdateDataProfile = userUpdateDataProfile;
            _userGetPicProfile = userGetPicProfile;
            _cuponActivate = cuponActivate;
            _balanceGet = balanceGet;


        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            const long MaxFileSize = 5 * 1024 * 1024;
            if (file == null || file.Length == 0|| file.Length > MaxFileSize)
                return BadRequest("Файл отсутствует или имеет некорректный размер.");

            try
            {

                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
                {
                    using (Stream stream = file.OpenReadStream())
                    {

                        var imageDto = new ImageDTO
                        {
                            Stream = stream,
                            Name = file.FileName
                        };

                        await _userUpdateDataProfile.Update(imageDto, userId);

                        return Ok("Файл успешно обработан.");
                    }
                }

                return Unauthorized("Не удалось определить пользователя.");
            }
            catch (Exception ex)
            {
                // Логируем исключение, если необходимо
                // _logger.LogError(ex, "Ошибка при загрузке файла");

                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {
            IResponse<UserDTO> response = await _userRegisterService.Register(userDTO);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpPost]
        [Route("emailsendcode")]
        public async Task<IActionResult> emailsendcode(UserDTO userDTO)
        {
            IResponse<UserDTO> response = await _userEmailCodeSend.SendCode(userDTO);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpPost]
        [Route("emailconfirmcode")]
        public async Task<IActionResult> emailconfirmcode(UserDTO userDTO)
        {
            IResponse<UserDTO> response = await _userEmailConfirm.ConfirmCode(userDTO);
            Console.WriteLine("Cheeeee");
            return ResponseHandler.HandleResponse(response);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> loginUser([FromBody] UserDTO userDTO)
        {
            IResponse<UserDTO> response = await _userLoginService.Login(userDTO);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> GetData(string userName)
        {
           
            var userId = HttpContext.Items["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            IResponse<User> response = await _userGetData.Get(userName,userId);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalance()
        {
           
            var userId = HttpContext.Items["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            IResponse<UserDTO> response = await _balanceGet.Get(userId);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpPost]
        [Route("ActivateCupon")]
        public async Task<IActionResult> Activate(string cupon)
        {
           
            var userId = HttpContext.Items["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            IResponse<UserDTO> response = await _cuponActivate.Activate(userId,cupon);
            return ResponseHandler.HandleResponse(response);
        }
        [HttpGet]
        [Route("Profile1")]
        public async Task<IActionResult> GetImage(string UserName) 
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is string userId)
            {

                IResponse<ImageDTO> response  = await _userGetPicProfile.Get(UserName,userId);
                if(response.StatusCode != 200)
                {   
                    return NotFound();
                }
                if (response.Data.Stream == null)
                {
                    return Ok("Изображение не найдено.");
                }

            
                string mimeType = "image/png"; 


                return File(response.Data.Stream , mimeType);
            }
            return BadRequest("");
        }



    }
}
