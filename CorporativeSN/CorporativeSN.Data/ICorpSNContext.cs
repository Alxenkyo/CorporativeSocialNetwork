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
        DbSet<Chat> Chats { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<MessageStatus> MessageStatuses { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Event> Events { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
