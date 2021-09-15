using CD_IdentityManagement.DBContext;
using CD_IdentityManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CD_IdentityManagement.Controllers
{
    [Route("api/contoller")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = usermanager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Register userRegisterDetails)
        {
            var userExist = await userManager.FindByIdAsync(userRegisterDetails.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }
            ApplicationUser newuser = new ApplicationUser()
            {
                Email = userRegisterDetails.Email,
                UserName = userRegisterDetails.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var results = await userManager.CreateAsync(newuser, userRegisterDetails.Password);
            if (!results.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Creation Failed" });
            }
            return Ok(new Response { Status = "Success", Message = "User Created Successfully" });
        }
    }
}
