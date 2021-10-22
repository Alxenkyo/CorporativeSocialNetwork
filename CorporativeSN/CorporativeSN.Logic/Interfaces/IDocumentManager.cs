using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IDocumentManager
    {
        Task<PagedResult<DocumentDTO>> GetDocumentsAsync(int userId, CancellationToken cancellationToken = default);
        Task<DocumentDTO> GetDocumentAsync(int docId, CancellationToken cancellationToken = default);
        Task AddDocumentAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<DocumentDTO> UpdateDocumentAsync(DocumentDTO doc, CancellationToken cancellationToken = default);
        Task DeleteDocumentAsync(int docId, CancellationToken cancellationToken = default);
        Task<PagedResult<DocumentDTO>> GetDocListAsync(int userId, CancellationToken cancellationToken = default);
        Task<DocumentDTO> DownloadDocAsync(int docId, CancellationToken cancellationToken = default);
    }
}
