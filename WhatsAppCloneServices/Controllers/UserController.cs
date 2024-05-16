using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatsAppCloneServices.Models;
using WhatsAppCloneServices.Data.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WhatsAppCloneServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    SentMessages = new List<Message>(),
                    ReceivedMessages = new List<Message>(),
                    Contacts = new List<Contact>(),
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    var userInfo = new
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        UserId = user.Id,
                    };

                    _logger.LogInformation("Sender {Username} registered successfully.", model.Username);
                    return Ok(new { message = "Sender registered successfully", user = userInfo});
                }

                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to register {Username}.", model.Username);
                return StatusCode(500, "An Error Occured Please Try Again");
            }

       

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var claims = new List<Claim>
                 {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.Name, user.UserName)
                 };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    _logger.LogInformation("Sender {Username} Logged In successfully.", model.Username);
                    return Ok(new { message = "Login successful", userId = user.Id, userName = user.UserName });
                }

                return BadRequest(new { message = "Invalid login attempt" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to register {Username}.", model.Username);
                return StatusCode(500, "An Error Occured Please Try Again");
            }


        }
    }
}
