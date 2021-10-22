using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using CorporativeSN.Api.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorporativeSN.Api.Controllers
{
    [EnableCors("MyAllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager _messageManager;
        private readonly IChatManager _chatManager;
        private readonly IHubContext<ChatHub> _hub;
        public MessageController(IMessageManager messageManager, IHubContext<ChatHub> hubContext, IChatManager chatManager)
        {
            _messageManager = messageManager;
            _hub = hubContext;
            _chatManager = chatManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesAsync(
            [FromQuery] string search,
            [FromQuery] int? fromIndex = default,
            [FromQuery] int? toIndex = default,
            CancellationToken cancellationToken = default)
        {
            var result = await _messageManager.GetMessagesAsync(search, fromIndex, toIndex, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessageAsync(
            int messageId,
            CancellationToken cancellationToken = default)
        {
            var result = await _messageManager.GetMessageAsync(messageId, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost()]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> SendMessageAsync(
           [FromBody]MessageDTO message,
           CancellationToken cancellationToken = default)
        {
            
            message.CreatedDate = DateTime.Now;
            message.CreatorId = int.Parse(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            var chat = await _chatManager.GetChatAsync(message.ChatId);
            var result = await _messageManager.CreateMessageAsync(message, cancellationToken);
            message.CreatorName =  _messageManager.GetMessageAsync(result.Id).Result.CreatorName;
            await _hub.Clients.All.SendAsync("Notify", message,cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(
            int messageId,
            CancellationToken cancellationToken = default)
        {
            await _messageManager.DeleteMessageAsync(messageId, cancellationToken);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateMessageAsync(
            MessageDTO message,
            CancellationToken cancellationToken = default)
        {
            var result = await _messageManager.UpdateMessageAsync(message, cancellationToken);
            return Ok(result);
        }
    }
}
