using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoreRepository;
using CoreRepository.Models;

namespace HappyCompany_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserContraller : BaseController
    {
        //private readonly ILogger<UserContraller> _logger; , ILogger<UserContraller> logger
        public UserContraller(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
            //_logger = logger;
        }

        [HttpPost]
        [Route("DeleteUser")]

        public IActionResult Delete([FromQuery] string userId)
        {
            try
            {
                User user = _unitOfWork.Users.GetEntity((_user) => _user.Id.Equals(new Guid(userId)));
                if (!user.Role.Equals("Admin") && userId != null)
                {
                    _unitOfWork.Users.DeleteEntity((x) => x.Id.Equals(new Guid(userId)));
                    _unitOfWork.Complete();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUsers")]
      
        public IActionResult GetUsers([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                int count = 0;
                List<User> users = _unitOfWork.Users.GetPaged(page, pageSize, out count).ToList();
                return Ok(new { users, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetRoles")]
       
        public IActionResult GetRoles()
        {
            try
            {
                int count = 0;
                List<Role> roles = _unitOfWork.Roles.GetPaged(0, int.MaxValue, out count).ToList();
                return Ok(new { roles, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUser")]
        
        public IActionResult GetUser([FromQuery] string userId)
        {
            try
            {
                User user = _unitOfWork.Users.GetEntity((user) => user.Id.Equals(new Guid(userId)));
                return Ok( user );
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
