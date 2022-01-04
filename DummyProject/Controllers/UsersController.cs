using BusinessAccessLayer;
using BusinessAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DummyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private IUserService _userManager;
        private readonly ApplicationSettings _applicationSettings;
        public UsersController(IUserService loginModel, IOptions<ApplicationSettings> options, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userManager = loginModel;
            _applicationSettings = options.Value;
        }

        [HttpPost]
        [Route(HelperURLConst.login)]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _userManager.FindByName(userName);
            if (user != null && _userManager.CheckPassword(user, password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity
                    (
                        new Claim[]
                        {
                            new Claim(HelperConst.userID, user.UserId.ToString()),
                            new Claim(HelperConst.userName, user.UserName.ToString()),
                            new Claim(HelperConst.userType, user?.UserType?.ToString() ?? "")
                        }
                    ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature
                    )
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                _logger.LogInformation(MessageConst.userPasswordIncorrect);
                return BadRequest(new { message = MessageConst.userPasswordIncorrect });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            if (userModel.UserId > 0)
            {
                _logger.LogInformation(MessageConst.insertExplicit);
                return BadRequest(new { message = MessageConst.insertExplicit });
            }
            var userValidate = await _userManager.FindByName(userModel.UserName);
            if (userValidate != null)
            {
                _logger.LogInformation(MessageConst.userNameExist);
                return BadRequest(new { message = MessageConst.userNameExist });
            }
            else
            {
                var user = await _userManager.AddUserAsync(userModel);
                return Ok(user);
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UserModel userModel)
        {
            if (userModel.UserId == 0)
            {
                _logger.LogInformation(MessageConst.userIdNotPass);
                return BadRequest(new { message = MessageConst.userIdNotPass });
            }
            var isUser = await _userManager.UpdateUserAsync(userModel);
            if (!isUser)
            {
                _logger.LogInformation(MessageConst.userNameExist);
                return BadRequest(new { message = MessageConst.userNameExist });
            }
            else
            {
                var user = await _userManager.GetByIdAsync(userModel.UserId);
                if (user == null)
                {
                    _logger.LogError(MessageConst.userNotFound);
                    return NotFound(new { message = MessageConst.userNotFound });
                }
                return Ok(user);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(long userId)
        {
            var userValidate = await _userManager.DeleteUserAsync(userId);

            if (!userValidate)
            {
                _logger.LogError(MessageConst.userNotFound);
                return NotFound(new { message = MessageConst.userNotFound });
            }
            else
            {
                return Ok(new { message = MessageConst.userDeleted });
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetAllAsync();
            return Ok(user);
        }


        [Authorize]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            var userValidate = await _userManager.GetByIdAsync(userId);
            if (userValidate == null)
            {
                _logger.LogError(MessageConst.userNotFound);
                return NotFound(new { message = MessageConst.userNotFound });
            }
            else
            {
              return Ok(userValidate);
            }
        }

    }
}