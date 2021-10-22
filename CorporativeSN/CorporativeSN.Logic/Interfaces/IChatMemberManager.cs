using CorporativeSN.Data;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IChatMemberManager
    {
        Task<PagedResult<ChatMemberDTO>> GetChatMembersAsync(int userId, CancellationToken cancellationToken = default);
        //Task<ChatMemberDTO> GetChatMemberAsync(int chatId, CancellationToken cancellationToken = default);
        Task<ChatMemberDTO> CreateChatMemberAsync(ChatMemberDTO chatMember, CancellationToken cancellationToken = default);
        Task<ChatMemberDTO> UpdateChatMemberAsync(ChatMemberDTO chatMember, CancellationToken cancellationToken = default);
        Task DeleteChatMemberAsync(int chmId, CancellationToken cancellationToken = default);
    }
}
