using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorporativeSN.Api.Controllers
{
    [EnableCors("MyAllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(
            [FromQuery] string search,
            [FromQuery] int? fromIndex = default,
            [FromQuery] int? toIndex = default,
            CancellationToken cancellationToken = default)
        {
            var result = await _userManager.GetUsersAsync(search, fromIndex, toIndex, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            var result = await _userManager.GetUserAsync(userId, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken = default)
        {
            var result = await _userManager.GetUserProfile(User.Identity.Name, cancellationToken);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateUserAsync(
           UserDTO user,
           CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateUserAsync(user, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            await _userManager.DeleteUserAsync(userId, cancellationToken);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUserAsync(
            UserDTO user,
            CancellationToken cancellationToken = default)
        {
            var result = await _userManager.UpdateUserAsync(user, cancellationToken);
            return Ok(result);
        }
    }
}
