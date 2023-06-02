
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreRepository;
using CoreRepository.Models;
using HappyCompany_API.Helper;

namespace HappyCompany_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IJWT _jwt;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IJWT jwt, IConfiguration configuration, IUnitOfWork unitOfWork, ILogger<AuthController> logger) : base(configuration, unitOfWork)
        {
            _logger = logger;
            _jwt = jwt;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {

                if (user.Id != Guid.Empty)
                {
                    _unitOfWork.Users.Update(user);
                }
                else
                {
                    User RegisterUser = _unitOfWork.Users.GetEntity((_user) => _user.Email.Equals(user.Email));

                    if (RegisterUser == null)
                    {
                        _unitOfWork.Users.Add(user);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                _unitOfWork.Complete();
                return Ok(new { message="Success"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message="error"});
            }
        }



        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin user)
        {
            try
            {
                User LoginUser = _unitOfWork.Users.GetEntity((_user) => _user.Email.Equals(user.username) && _user.Password.Equals(user.password));

                if (LoginUser != null)
                {
                    if (LoginUser.Active)
                    {
                        string token = _jwt.GenerateToken(LoginUser);
                        return Ok(new
                        {
                            token,
                            id= LoginUser.Id,
                            role = LoginUser.Role,
                            username = LoginUser.Email
                        });
                    }
                    else
                    {
                        return Ok(new { message = "your acount is disabled, Please contact support" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] UserPass userPass)
        {
            try
            {
                User user = _unitOfWork.Users.GetEntity((_user) => _user.Id.Equals(userPass.Id));
                    user.Password = userPass.Password;
                    _unitOfWork.Complete();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
