using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using CorporativeSN.Api.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace CorporativeSN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;
        IWebHostEnvironment _appEnvironment;
        public DocumentController(IDocumentManager documentManager, IWebHostEnvironment env)
        {
            _documentManager = documentManager;
            _appEnvironment = env;
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetDocsAsync(
        //    CancellationToken cancellationToken = default)
        //{
        //    int userId = int.Parse(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        //    var result = await _documentManager.GetDocumentsAsync(userId, cancellationToken);
        //    return Ok(result);
        //}

        //[HttpGet("{docId}")]
        //public async Task<IActionResult> GetDocAsync(
        //    int docId,
        //    CancellationToken cancellationToken = default)
        //{
        //    var result = await _documentManager.GetDocumentAsync(docId, cancellationToken);
        //    return Ok(result);
        //}

        [HttpPost()]
        public async Task<IActionResult> AddDocAsync(
           [FromBody] IFormFile file,
           CancellationToken cancellationToken = default)
        {
            /*var result = */await _documentManager.AddDocumentAsync(file, cancellationToken);
            return Ok(/*result*/);
        }

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteDocAsync(
            int docId,
            CancellationToken cancellationToken = default)
        {
            await _documentManager.DeleteDocumentAsync(docId, cancellationToken);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateDocAsync(
            DocumentDTO doc,
            CancellationToken cancellationToken = default)
        {
            var result = await _documentManager.UpdateDocumentAsync(doc, cancellationToken);
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("docslist")]
        public async Task<IActionResult> GetDocListAsync(CancellationToken cancellationToken = default)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            var result = await _documentManager.GetDocListAsync(userId, cancellationToken);
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("download")]
        public async Task<IActionResult> DownloadDocAsync(int docId, CancellationToken cancellationToken = default)
        {
            var result = await _documentManager.DownloadDocAsync(docId, cancellationToken);
            string path = Path.Combine(_appEnvironment.WebRootPath, "docs/") + result.FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes,"application/octet-stream", result.FileName);
        }
    }
}
