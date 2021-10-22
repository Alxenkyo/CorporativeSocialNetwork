using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using CorporativeSN.Api.Hubs;

namespace CorporativeSN.Api.Controllers
{
    [EnableCors("MyAllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private const string IdClaim = "Id";
        private readonly IUserManager _userManager;
        private readonly IHubContext<ChatHub> _hub;
        public LoginController(IUserManager userManager, IHubContext<ChatHub> hub)
        {
            _userManager = userManager;
            _hub = hub;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid login or password." });
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt
            };
            var role = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            Response.Headers.Add("Access-Control-Expose-Headers", "X-User-Type");
            Response.Headers.Add("X-User-Type", role);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> CheckAuth(string connectionId, CancellationToken cancellationToken = default)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            var user = await _userManager.GetUserAsync(userId,  cancellationToken);
            foreach(var item in user.ChatMembers)
            {
                await _hub.Groups.AddToGroupAsync(connectionId, item.ChatName, cancellationToken);
            }
            return Ok();
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password, CancellationToken cancellationToken = default)
        {
            UserDTO person = await _userManager.AuthUserAsync(username, password, cancellationToken);
            if (person != null)
            {
                UserTypeDTO role = await _userManager.GetUserRole(person, cancellationToken);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name),
                    new Claim(IdClaim, person.Id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
