using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using CorporativeSN.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Managers
{
    public class EventManager : IEventManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public EventManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }
        
        public async Task<EventDTO> CreateEventAsync(EventDTO eventNote, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<Event>(eventNote);
            _corpSNContext.Events.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EventDTO>(add);
        }

        public async Task DeleteEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            var eventNote = await _corpSNContext.Events.FirstOrDefaultAsync(x => x.Id == eventId, cancellationToken);
            if (eventNote != null)
            {
                _corpSNContext.Events.Remove(eventNote);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<EventDTO> GetEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            var eventNote = await _corpSNContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId, cancellationToken);
            return _mapper.Map<EventDTO>(eventNote);
        }

        public async Task<PagedResult<EventDTO>> GetEventsAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Events.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()));
            }
            if (fromIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value);
            }
            query = query.OrderBy(x => x.Name);
            var total = await query.CountAsync(cancellationToken);
            if (fromIndex.HasValue && toIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            }
            //var items = _mapper.ProjectTo<ChatDTO>(query).ToArrayAsync(cancellationToken);
            var items = _mapper.Map<IEnumerable<EventDTO>>(query);
            return new PagedResult<EventDTO> { Items = (IEnumerable<EventDTO>)items, Total = total };
        }

        public async Task<EventDTO> UpdateEventAsync(EventDTO eventNote, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Events.FirstOrDefaultAsync(x => x.Id == eventNote.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(eventNote, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return eventNote;
        }
    }
}
