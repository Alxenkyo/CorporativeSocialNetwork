using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public UserManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<User>(user);
            _corpSNContext.Users.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDTO>(add);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if (user != null)
            {
                _corpSNContext.Users.Remove(user);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<UserDTO> GetUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<PagedResult<UserDTO>> GetUsersAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Users.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()));
            }
            if (fromIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value);
            }
            query = query.OrderBy(x => x.Name);
            var total = await query.CountAsync(cancellationToken);
            if (fromIndex.HasValue && toIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            }
            //var items = _mapper.ProjectTo<ChatDTO>(query).ToArrayAsync(cancellationToken);
            var items = _mapper.Map<IEnumerable<UserDTO>>(query);
            return new PagedResult<UserDTO> { Items = (IEnumerable<UserDTO>)items, Total = total };
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(user, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return user;
        }
        
        public async Task<UserDTO> AuthUserAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login && x.Password == password, cancellationToken);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserTypeDTO> GetUserRole(UserDTO user, CancellationToken cancellationToken = default)
        {
            var role = await _corpSNContext.UserTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.UserTypeId, cancellationToken);
            return _mapper.Map<UserTypeDTO>(role);
        }
    }
}
