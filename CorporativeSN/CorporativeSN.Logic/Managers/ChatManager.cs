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
    public class ChatManager : IChatManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public ChatManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }
        //add async to all metods
        public async Task<ChatDTO> CreateChatAsync(ChatDTO chat, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<Chat>(chat);
            _corpSNContext.Chats.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ChatDTO>(add);
        }

        public async Task DeleteChatAsync(int chatId, CancellationToken cancellationToken = default)
        {
            var chat = await _corpSNContext.Chats.FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken);
            if (chat != null)
            {
                _corpSNContext.Chats.Remove(chat);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ChatDTO> GetChatAsync(int chatId, CancellationToken cancellationToken = default)
        {
            var chat = await _corpSNContext.Chats.AsNoTracking().FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken);
            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<PagedResult<ChatDTO>> GetChatsAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Chats.AsNoTracking();
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
            var items =  _mapper.Map<IEnumerable<ChatDTO>>(query);
            return new PagedResult<ChatDTO> { Items = (IEnumerable<ChatDTO>)items, Total = total };
        }

        public async Task<ChatDTO> UpdateChatAsync(ChatDTO chat, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Chats.FirstOrDefaultAsync(x => x.Id == chat.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(chat, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return chat;
        }
    }
}
