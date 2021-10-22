using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IChatManager
    {
        Task<PagedResult<ChatDTO>> GetChatsAsync(int userId, CancellationToken cancellationToken = default);
        Task<ChatDTO> GetChatAsync(int chatId, CancellationToken cancellationToken = default);
        Task<ChatDTO> CreateChatAsync(ChatDTO chat, CancellationToken cancellationToken = default);
        Task<ChatDTO> UpdateChatAsync(ChatDTO chat, CancellationToken cancellationToken = default);
        Task DeleteChatAsync(int chatId, int userId, CancellationToken cancellationToken = default);

        Task<ChatDTO> CreatePersonalChatAsync(ChatDTO chat, CancellationToken cancellationToken = default);
    }
}
