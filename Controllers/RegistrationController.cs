using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIQuiz.Models;
using WebAPIQuiz.Utilities;

namespace WebAPIQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [APIKeyAuthorization]
    public class RegistrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public RegistrationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationModel model)
        {
            var checkUser = await _userManager.FindByNameAsync(model.Username);
            if (checkUser != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exist" });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "Password does not match" });
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var res = await _userManager.CreateAsync(user, model.Password);
            if (!res.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User registration error" });
            }

            return Ok(new ResponseModel { Status = "Success", Message = "User Registered" });
        }
    }
}
