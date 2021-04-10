using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Managers
{
    public class MessageManager : IMessageManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public MessageManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }
        public async Task<MessageDTO> CreateMessageAsync(MessageDTO message, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<Message>(message);
            _corpSNContext.Messages.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<MessageDTO>(add);
        }

        public async Task DeleteMessageAsync(int messageId, CancellationToken cancellationToken = default)
        {
            var message = await _corpSNContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken);
            if (message != null)
            {
                _corpSNContext.Messages.Remove(message);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<MessageDTO> GetMessageAsync(int messageId, CancellationToken cancellationToken = default)
        {
            var message = await _corpSNContext.Chats.AsNoTracking().FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken);
            return _mapper.Map<MessageDTO>(message);
        }

        public async Task<PagedResult<MessageDTO>> GetMessagesAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Messages.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Text.ToLower().Contains(search.ToLower()));
            }
            if (fromIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value);
            }
            //query = query.OrderBy(x => x.Name);
            var total = await query.CountAsync(cancellationToken);
            if (fromIndex.HasValue && toIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            }
            //var items = _mapper.ProjectTo<ChatDTO>(query).ToArrayAsync(cancellationToken);
            var items = _mapper.Map<IEnumerable<MessageDTO>>(query);
            return new PagedResult<MessageDTO> { Items = (IEnumerable<MessageDTO>)items, Total = total };
        }

        public async Task<MessageDTO> UpdateMessageAsync(MessageDTO message, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Chats.FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(message, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return message;
        }
    }
}

