using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using WebAPIQuiz.Database;
using WebAPIQuiz.Models;
using WebAPIQuiz.Utilities;

namespace WebAPIQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [APIKeyAuthorization]
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _appDBContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager,AppDBContext appDBContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _appDBContext = appDBContext;
            _signInManager = signInManager;
        }
        [NonAction]
        public bool IsEmail(string emailadd)
        {
            try
            {
                MailAddress m = new(emailadd);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var uName = model.Username;

            if (IsEmail(model.Username))
            {
                var uData = _appDBContext.Users.FirstOrDefault(x => x.Email == model.Username);
                if (uData != null)
                {
                    uName = uData.UserName;
                }

            }
            var res = _signInManager.PasswordSignInAsync(uName!, model.Password, false, false).GetAwaiter().GetResult();

            if (res.Succeeded)
            {
                return Ok(new ResponseModel { Status = "Success", Message = "User logged in" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User login error" });
            }
        }


        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            try
            {
                _signInManager.SignOutAsync().GetAwaiter().GetResult();
                return Ok(new ResponseModel { Status = "Success", Message = "User logged ou" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "log out error" });
            }
        }
    }
}
