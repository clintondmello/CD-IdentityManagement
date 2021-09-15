using CD_IdentityManagement.DBContext;
using CD_IdentityManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CD_IdentityManagement.Controllers
{
    [Route("api/contoller")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationUser> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> usermanager, RoleManager<ApplicationUser> roleManager, IConfiguration configuration)
        {
            this.userManager = usermanager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Register([FromBody] Register userRegisterDetails)
        {
            var userExist = await userManager.FindByIdAsync(userRegisterDetails.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }
        }


    }
}
