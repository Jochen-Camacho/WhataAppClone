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
    public class MessageController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly WhatsAppCF whatsAppCF;

        public MessageController(WhatsAppCF whatsAppCF, UserManager<User> userManager)
        {
            this.whatsAppCF = whatsAppCF;
            _userManager = userManager;
        }

        [HttpPost("send_message")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound(new { message = "Sender not logged in." });
            }

            var user = await whatsAppCF.Users.Include(u => u.SentMessages).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(new { message = "Sender not found" });
            }

            var recipient = await whatsAppCF.Users.Include(u => u.ReceivedMessages).FirstOrDefaultAsync(c => c.Id == model.RecipientUserId);
            if (recipient == null)
            {
                return NotFound(new { message = "Recipient not found." });
            }

            var message = new Message
            {
                Content = model.Content,
                Timestamp = DateTime.UtcNow, 
                SenderId = user.Id,
                Sender = user,
                RecipientUserId = model.RecipientUserId,
                Recipient = recipient
            };

            user.SentMessages.Add(message);
            recipient.ReceivedMessages.Add(message);

            whatsAppCF.Messages.Add(message);
            await whatsAppCF.SaveChangesAsync();

            return Ok(new { message = "Message Sent" });

        }

        [HttpPost("get_messages_by_user")]
        public async Task<IActionResult> GetMessages([FromBody] ChatDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound(new { message = "Sender not logged in." });
            }

            var user = await whatsAppCF.Users.Include(u => u.SentMessages).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(new { message = "Sender not found" });
            }

            var recipient = await whatsAppCF.Users.Include(u => u.ReceivedMessages).FirstOrDefaultAsync(c => c.Id == model.RecipientId);
            if (recipient == null)
            {
                return NotFound(new { message = "Recipient not found." });
            }

            var messages = await whatsAppCF.Messages.
                Where(m => (m.SenderId == user.Id && m.RecipientUserId == recipient.Id) || (m.SenderId == recipient.Id && m.RecipientUserId == user.Id))
                .OrderBy(m => m.Timestamp)
                .Select(c => new Message
                {
                    MessageId = c.MessageId,
                    SenderId = c.SenderId,
                    Sender = c.Sender,
                    Content = c.Content,
                    Recipient = c.Recipient,
                    RecipientUserId = c.RecipientUserId,
                    Timestamp = c.Timestamp,
                })
                .ToListAsync();

            return Ok(messages);
        }
    }
}
