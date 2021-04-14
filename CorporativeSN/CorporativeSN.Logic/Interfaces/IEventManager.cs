using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IEventManager
    {
        Task<PagedResult<EventDTO>> GetEventsAsync(string search, int? fromIndex = default, int? toIndex = default, CancellationToken cancellationToken = default);
        Task<EventDTO> GetEventAsync(int eventId, CancellationToken cancellationToken = default);
        Task<EventDTO> CreateEventAsync(EventDTO eventNote, CancellationToken cancellationToken = default);
        Task<EventDTO> UpdateEventAsync(EventDTO eventNote, CancellationToken cancellationToken = default);
        Task DeleteEventAsync(int eventId, CancellationToken cancellationToken = default);
    }
}
