using CorporativeSN.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Data
{
    public interface ICorpSNContext
    {
        DbSet<Chats> Chats { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        DbSet<UsersMessages> UsersMessages { get; set; }
        //DbSet<MessageStatus> MessageStatuses { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<UserTypes> UserTypes { get; set; }
        DbSet<Departments> Departments { get; set; }
        DbSet<UserEvents> UserEvents { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
