using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Managers
{
    public class ChatMemberManager : IChatMemberManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public ChatMemberManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }

        public async Task<ChatMemberDTO> CreateChatMemberAsync(ChatMemberDTO chatMember, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<ChatMember>(chatMember);
            _corpSNContext.ChatMembers.Add(add);

            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ChatMemberDTO>(add);
        }

        public async Task DeleteChatMemberAsync(int chmId, CancellationToken cancellationToken = default)
        {
            var member = await _corpSNContext.ChatMembers.FirstOrDefaultAsync(x => x.Id == chmId, cancellationToken);
            if (member != null)
            {
                _corpSNContext.ChatMembers.Remove(member);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PagedResult<ChatMemberDTO>> GetChatMembersAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatMemberDTO> UpdateChatMemberAsync(ChatMemberDTO chatMember, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
