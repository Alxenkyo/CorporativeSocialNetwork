using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporativeSN.Data;
using System.Diagnostics.CodeAnalysis;
using CorporativeSN.Models;

namespace CorporativeSN.Api
{
    public class CorpSNContext : DbContext, ICorpSNContext
    {
        public CorpSNContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set ; }
        public DbSet<Message> Messages { get; set ; }
        public DbSet<MessageStatus> MessageStatuses { get ; set ; }
        public DbSet<User> Users { get; set ; }
        public DbSet<UserType> UserTypes { get ; set ; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<UserType>().HasKey(x => x.Id);
            modelBuilder.Entity<UserType>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserType>().HasMany(x => x.Users).WithOne().HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<MessageStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<MessageStatus>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Chat>().HasKey(x => x.Id);
            modelBuilder.Entity<Chat>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Chat>().HasMany(x => x.Members).WithOne().HasForeignKey(x => x.ChatId);

            modelBuilder.Entity<ChatMember>().HasKey(x => x.Id);
            modelBuilder.Entity<ChatMember>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
