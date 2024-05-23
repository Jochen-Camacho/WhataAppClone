using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WhatsAppCloneServices.Data;
using WhatsAppCloneServices.Data.DTOs;
using WhatsAppCloneServices.Models;

namespace WhatsAppCloneServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {

        private readonly UserManager<User> _userManager;
        private WhatsAppCF whatsAppCF;

        public ContactController(UserManager<User> userManager, WhatsAppCF whatsAppCF)
        {
            _userManager = userManager;
            this.whatsAppCF = whatsAppCF;   
        }


        [HttpPost("add_contact")]
        public async Task<IActionResult> AddContact([FromBody] ContactDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Sender is not logged in.");
            }

            var currentUser = await whatsAppCF.Users
                .Include(u => u.Contacts)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (currentUser == null)
            {
                return NotFound("Sender not found.");
            }

            var contactUser = await whatsAppCF.Users
                .FirstOrDefaultAsync(u => u.UserName == model.Username);

            if (contactUser == null)
            {
                return NotFound("Contact user not found.");
            }

            bool contactExists = currentUser.Contacts
                .Any(c => c.ContactUserId == contactUser.Id);

            if (contactExists)
            {
                return BadRequest("Contact already added.");
            }

            var contact = new Contact
            {
                User = currentUser,
                UserId = userId,
                ContactUserId = contactUser.Id,
                ContactUser = contactUser,
                DateAdded = DateTime.UtcNow,
            };

            currentUser.Contacts.Add(contact);

            await whatsAppCF.Contacts.AddAsync(contact);
            await whatsAppCF.SaveChangesAsync();

            return Ok($"Contact added successfully for user {userId}");
        }

        [HttpGet("get_contacts")]
        public async Task<IActionResult> GetContacts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Sender is not logged in.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Sender not found.");
            }

            var userContacts = await whatsAppCF.Contacts
                .Where(c => c.UserId == userId)
                .Select(c => new
                {
                    c.ContactId,
                    c.UserId,
                    c.ContactUserId,
                    ContactUser = new
                    {
                        c.ContactUser.Id,
                        c.ContactUser.UserName,
                        c.ContactUser.Email,
                        c.ContactUser.DateCreated,
                        c.ContactUser.ProfileImage
                    },
                    c.DateAdded
                })
                .ToListAsync();

            var unknownContacts = await whatsAppCF.Contacts
                .Where(c => c.ContactUserId == userId)
                .Select(c => new
                {
                    c.ContactId,
                    c.UserId,
                    c.ContactUserId,
                    ContactUser = new
                    {
                        c.User.Id,
                        c.User.UserName,
                        c.User.Email,
                        c.User.DateCreated,
                        c.User.ProfileImage
                    },
                    c.DateAdded
                })
                .ToListAsync();

            var contacts = new List<object> { userContacts, unknownContacts };

            return Ok(contacts);
        }

    }
}
