using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporativeSN.Data.Models;
using System.Diagnostics.CodeAnalysis;
using CorporativeSN.Data;

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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasMany(x => x.CreatedEvents).WithOne().HasForeignKey(x => x.CreatorId);
            modelBuilder.Entity<User>().HasMany(x => x.ChatMembers).WithOne().HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserType>().HasKey(x => x.Id);
            modelBuilder.Entity<UserType>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserType>().HasMany(x => x.Users).WithOne().HasForeignKey(x => x.UserTypeId);

            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<MessageStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<MessageStatus>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MessageStatus>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.StatusId);

            modelBuilder.Entity<Chat>().HasKey(x => x.Id);
            modelBuilder.Entity<Chat>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Chat>().HasMany(x => x.Members).WithOne().HasForeignKey(x => x.ChatId);
            modelBuilder.Entity<Chat>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.ChatId);

            modelBuilder.Entity<ChatMember>().HasKey(x => x.Id);
            modelBuilder.Entity<ChatMember>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Department>().HasKey(x => x.Id);
            modelBuilder.Entity<Department>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().HasMany(x => x.Members).WithOne().HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}
