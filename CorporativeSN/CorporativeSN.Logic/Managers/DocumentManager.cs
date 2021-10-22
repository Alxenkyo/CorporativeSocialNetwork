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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CorporativeSN.Logic.Managers
{
    public class DocumentManager : IDocumentManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public DocumentManager(ICorpSNContext corpSNContext, IMapper mapper, IHostingEnvironment hosting)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
            _hostingEnvironment = hosting;
        }

        public async Task AddDocumentAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Docs");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            //var add = _mapper.Map<Documents>(doc);
            //add.FilePath= Path.Combine(uploads, doc.File.FileName);
            //_corpSNContext.Documents.Add(add);
            //await _corpSNContext.SaveChangesAsync(cancellationToken);
            /*_mapper.Map<DocumentDTO>(add);*/
        }

        public async Task DeleteDocumentAsync(int docId, CancellationToken cancellationToken = default)
        {
            var doc = await _corpSNContext.Documents.FirstOrDefaultAsync(x => x.Id == docId, cancellationToken);
            if (doc != null)
            {
                _corpSNContext.Documents.Remove(doc);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<DocumentDTO> GetDocumentAsync(int docId, CancellationToken cancellationToken = default)
        {
            var doc = await _corpSNContext.Documents.AsNoTracking().FirstOrDefaultAsync(x => x.Id == docId, cancellationToken);
            return _mapper.Map<DocumentDTO>(doc); 
        }

        public async Task<PagedResult<DocumentDTO>> GetDocumentsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Documents
                .Include(x => x.Department)
                .AsEnumerable()
                .Where(x => x.DepartmentId == x.Department.Users.FirstOrDefault(c=>c.Id==userId).DepartmentId)
                //.Include(x => x.Messages)

                ;
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    query = query.Where(x =>
            //        x.Name.ToLower().Contains(search.ToLower()));
            //}
            //if (fromIndex.HasValue)
            //{
            //    query = query.Skip(fromIndex.Value);
            //}
            //query = query.OrderBy(x => x.Name);
            var total = query.Count();
            //if (fromIndex.HasValue && toIndex.HasValue)
            //{
            //    query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            //}
            //var items = _mapper.ProjectTo<ChatDTO>(query).ToArrayAsync(cancellationToken);
            var items = _mapper.Map<IEnumerable<DocumentDTO>>(query);
            return new PagedResult<DocumentDTO> { Items = (IEnumerable<DocumentDTO>)items, Total = total };
        }

        public async Task<DocumentDTO> UpdateDocumentAsync(DocumentDTO doc, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Documents.FirstOrDefaultAsync(x => x.Id == doc.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(doc, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return doc;
        }

        public async Task<PagedResult<DocumentDTO>> GetDocListAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = _corpSNContext.Users.FirstOrDefault(x => x.Id == userId);
            var query = _corpSNContext.Documents
                .Include(x => x.Department).ThenInclude(x => x.Users)
                .AsEnumerable()
                .Where(c =>c.DepartmentId==null || c.DepartmentId==user.DepartmentId);
            var total = query.Count();
            var items = _mapper.Map<IEnumerable<DocumentDTO>>(query);
            return new PagedResult<DocumentDTO> { Items = (IEnumerable<DocumentDTO>)items, Total = total };
        }

        public async Task<DocumentDTO> DownloadDocAsync(int docId, CancellationToken cancellationToken = default)
        {
            var item = await _corpSNContext.Documents.AsNoTracking().FirstOrDefaultAsync(x => x.Id == docId);
            return _mapper.Map<DocumentDTO>(item);
        }
    }
}
