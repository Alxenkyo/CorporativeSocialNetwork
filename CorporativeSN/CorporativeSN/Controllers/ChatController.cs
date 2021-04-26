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
    public class ChatController : ControllerBase
    {
        private readonly IChatManager _chatManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatManager chatManager, IHubContext<ChatHub> hubContext)
        {
            _chatManager = chatManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetChatsAsync(
            [FromQuery] string search,
            [FromQuery] int? fromIndex = default,
            [FromQuery] int? toIndex = default,
            CancellationToken cancellationToken = default)
        {
            var result = await _chatManager.GetChatsAsync(search, fromIndex, toIndex, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChatAsync(
            int chatId,
            CancellationToken cancellationToken = default)
        {
            var result = await _chatManager.GetChatAsync(chatId, cancellationToken);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateChatAsync(
           ChatDTO chat,
           CancellationToken cancellationToken = default)
        {
            var result = await _chatManager.CreateChatAsync(chat, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChatAsync(
            int chatId,
            CancellationToken cancellationToken = default)
        {
            await _chatManager.DeleteChatAsync(chatId, cancellationToken);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateChatAsync(
            ChatDTO chat,
            CancellationToken cancellationToken = default)
        {
            var result = await _chatManager.UpdateChatAsync(chat, cancellationToken);
            return Ok(result);
        }
    }
}
