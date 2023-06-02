using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreRepository;
using CoreRepository.Models;
using System.Security.Claims;

namespace HappyCompany_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected readonly IConfiguration _configuration;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly User _currentUser;
        public BaseController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _currentUser = GetCurrentUser();
        }


        protected User CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }


        private User GetCurrentUser()
        {
            var identity = HttpContext?.User?.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    FullName = GetClaimValue(ClaimTypes.NameIdentifier),
                    Email = GetClaimValue(ClaimTypes.Email),
                    Role = GetClaimValue(ClaimTypes.Role)
                };
            }

            return null;
        }

        private string GetClaimValue(string claimType)
        {
            return HttpContext.User.FindFirstValue(claimType);
        }

     
    }
}
