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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorporativeSN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager, IHubContext<ChatHub> hubContext)
        {
            _messageManager = messageManager;
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

        [HttpPost()]
        public async Task<IActionResult> SendMessageAsync(
           MessageDTO message,
           CancellationToken cancellationToken = default)
        {
            var result = await _messageManager.CreateMessageAsync(message, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteUserAsync(
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
