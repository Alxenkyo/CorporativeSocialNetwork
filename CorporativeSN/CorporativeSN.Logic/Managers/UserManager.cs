using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Data.Models;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        private static string GenerateSaltedHash(string text, string salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] saltbytes = Encoding.UTF8.GetBytes(salt);
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] plainTextWithSaltBytes = plainText.Concat(saltbytes).ToArray();           
            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }
        public async Task<UserDTO> CreateUserAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            
            var add = _mapper.Map<Users>(user);
            var salt = CreateSalt();
            add.PasswordSalt = salt;
            add.PasswordHash = GenerateSaltedHash(user.Password, salt);
            _corpSNContext.Users.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDTO>(add);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.Include(c=>c.ChatMembers).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if (user != null)
            {
                foreach(ChatMember member in user.ChatMembers)
                {
                    _corpSNContext.ChatMembers.Remove(member);
                }
                await _corpSNContext.SaveChangesAsync(cancellationToken);
                _corpSNContext.Users.Remove(user);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<UserDTO> GetUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserProfile(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _corpSNContext.Users.Include(x=>x.UserType).Include(x=>x.Department)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<PagedResult<UserDTO>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Users.Include(x=>x.Department).Include(x=>x.UserType).AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
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
            return _mapper.Map<UserDTO>(update);
        }
        
        public async Task<UserDTO> AuthUserAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var salt = _corpSNContext.Users.FirstOrDefault(x => x.Email == login);
            if (salt == null) { return null; }
            else {
                var passwordHash = GenerateSaltedHash(password, salt.PasswordSalt);
                var user = await _corpSNContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == login && x.PasswordHash.Equals(passwordHash), cancellationToken);
                return _mapper.Map<UserDTO>(user);
            }
        }

        public async Task<UserTypeDTO> GetUserRole(UserDTO user, CancellationToken cancellationToken = default)
        {
            var role = await _corpSNContext.UserTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.UserTypeId, cancellationToken);
            return _mapper.Map<UserTypeDTO>(role);
        }

        public async Task<PagedResult<UsersListDTO>> GetUsersList(CancellationToken cancellationToken = default)
        {
            var users =  await _corpSNContext.Users.Include(x => x.Department).Include(x => x.UserType).AsNoTracking().ToListAsync();
            var items = _mapper.Map<IEnumerable<UsersListDTO>>(users);
            var total =  users.Count();
            return new PagedResult<UsersListDTO> { Items = items, Total = total };
        }
    }
}
