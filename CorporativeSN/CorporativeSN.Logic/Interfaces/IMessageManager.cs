using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IMessageManager
    {
        Task<PagedResult<MessageDTO>> GetMessagesAsync(string search, int? fromIndex = default, int? toIndex = default, CancellationToken cancellationToken = default);
        Task<MessageDTO> GetMessageAsync(int messageId, CancellationToken cancellationToken = default);
        Task<MessageDTO> CreateMessageAsync(MessageDTO message, CancellationToken cancellationToken = default);
        Task<MessageDTO> UpdateMessageAsync(MessageDTO message, CancellationToken cancellationToken = default);
        Task DeleteMessageAsync(int messageId, CancellationToken cancellationToken = default);
    }
}
