using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using CorporativeSN.Api.Hubs;
using Microsoft.AspNetCore.Authorization;

namespace CorporativeSN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _depManager;
        public DepartmentController(IDepartmentManager depManager)
        {
            _depManager = depManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetChatsAsync(
            CancellationToken cancellationToken = default)
        {
            var result = await _depManager.GetDepartmentsAsync( cancellationToken);
            return Ok(result);
        }
    }
}
