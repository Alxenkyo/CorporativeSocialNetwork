using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventManager _eventManager;

        public EventController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsAsync(
            [FromQuery] string search,
            [FromQuery] int? fromIndex = default,
            [FromQuery] int? toIndex = default,
            CancellationToken cancellationToken = default)
        {
            var result = await _eventManager.GetEventsAsync(search, fromIndex, toIndex, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventAsync(
            int eventId,
            CancellationToken cancellationToken = default)
        {
            var result = await _eventManager.GetEventAsync(eventId, cancellationToken);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateEventAsync(
           EventDTO eventNote,
           CancellationToken cancellationToken = default)
        {
            var result = await _eventManager.CreateEventAsync(eventNote, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(
            int eventId,
            CancellationToken cancellationToken = default)
        {
            await _eventManager.DeleteEventAsync(eventId, cancellationToken);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateEventAsync(
            EventDTO eventNote,
            CancellationToken cancellationToken = default)
        {
            var result = await _eventManager.UpdateEventAsync(eventNote, cancellationToken);
            return Ok(result);
        }
    }
}
