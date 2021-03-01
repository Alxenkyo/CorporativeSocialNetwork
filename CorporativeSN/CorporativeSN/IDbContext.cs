using CorporativeSN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Api
{
    interface IDbContext
    { 
        DbSet<Chat> Chats { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<MessageStatus> MessageStatuses { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
