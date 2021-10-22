using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IUserManager
    {
        Task<PagedResult<UserDTO>> GetUsersAsync(CancellationToken cancellationToken = default);
        Task<UserDTO> GetUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<UserDTO> CreateUserAsync(UserDTO user, CancellationToken cancellationToken = default);
        Task<UserDTO> UpdateUserAsync(UserDTO user, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(int chatId, CancellationToken cancellationToken = default);
        Task<UserDTO> GetUserProfile(int username, CancellationToken cancellationToken = default);
        Task<UserDTO> AuthUserAsync(string login, string password, CancellationToken cancellation = default);
        Task <UserTypeDTO> GetUserRole(UserDTO user, CancellationToken cancellationToken = default);
        Task<PagedResult<UsersListDTO>> GetUsersList(CancellationToken cancellationToken = default);
    }
}
