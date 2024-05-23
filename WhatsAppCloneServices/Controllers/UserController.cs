using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatsAppCloneServices.Models;
using WhatsAppCloneServices.Data.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WhatsAppCloneServices.Data;

namespace WhatsAppCloneServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserController> _logger;
        private WhatsAppCF whatsAppCF;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserController> logger, WhatsAppCF whatsAppCF)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.whatsAppCF = whatsAppCF;
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
                    ProfileImage = model.ProfileImage,
                    DateCreated = DateTime.Now,
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
                    return Ok(new { message = "Login successful", userId = user.Id, userName = user.UserName, userImage = user.ProfileImage });
                }

                return BadRequest(new { message = "Invalid login attempt" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to register {Username}.", model.Username);
                return StatusCode(500, "An Error Occured Please Try Again");
            }
        }

        [HttpPost("upload_image")]
        public async Task<IActionResult> UploadImage([FromBody] byte[] image)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var user = whatsAppCF.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                user.ProfileImage = image;
                await whatsAppCF.SaveChangesAsync();
                return Ok("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to upload an image for {Username}.", user.UserName);
                return StatusCode(500, "An error occurred. Please try again.");
            }
        }
    }
}
