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
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var result = await _userManager.GetUsersAsync(cancellationToken);
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
            int userId = int.Parse(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            var result = await _userManager.GetUserProfile(userId, cancellationToken);
            return Ok(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost()]
        public async Task<IActionResult> CreateUserAsync(
           UserDTO user,
           CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateUserAsync(user, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            await _userManager.DeleteUserAsync(userId, cancellationToken);
            return Ok();
        }

        [Authorize()]
        [HttpPut()]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateUserAsync(
            [FromBody]UserDTO user,
            CancellationToken cancellationToken = default)
        {
            var result = await _userManager.UpdateUserAsync(user, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("profiles")]
        public async Task<IActionResult> GetUsersProfiles()
        {
            var result = await _userManager.GetUsersList();
            return Ok(result);
        }
    }
}
