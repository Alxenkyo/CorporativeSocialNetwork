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

        public DbSet<Chats> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set ; }
        public DbSet<UsersMessages> UsersMessages { get; set ; }
        //public DbSet<MessageStatus> MessageStatuses { get ; set ; }
        public DbSet<Users> Users { get; set ; }
        public DbSet<UserTypes> UserTypes { get ; set ; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(x => x.Id);
            modelBuilder.Entity<Users>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Users>().HasMany(x => x.CreatedEvents).WithOne().HasForeignKey(x => x.CreatorId);
            modelBuilder.Entity<Users>().HasMany(x => x.ChatMembers).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Users>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.CreatorId);
            modelBuilder.Entity<Users>().HasMany(x => x.CreatedChats).WithOne().HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<UserTypes>().HasKey(x => x.Id);
            modelBuilder.Entity<UserTypes>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserTypes>().HasMany(x => x.Users).WithOne().HasForeignKey(x => x.UserTypeId);

            modelBuilder.Entity<UsersMessages>().HasKey(x => x.Id);
            modelBuilder.Entity<UsersMessages>().Property(x => x.Id).ValueGeneratedOnAdd();

            //modelBuilder.Entity<MessageStatus>().HasKey(x => x.Id);
            //modelBuilder.Entity<MessageStatus>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<MessageStatus>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.StatusId);

            modelBuilder.Entity<Chats>().HasKey(x => x.Id);
            modelBuilder.Entity<Chats>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Chats>().HasMany(x => x.Members).WithOne().HasForeignKey(x => x.ChatId);
            modelBuilder.Entity<Chats>().HasMany(x => x.Messages).WithOne().HasForeignKey(x => x.ChatId);

            modelBuilder.Entity<ChatMember>().HasKey(x => x.Id);
            modelBuilder.Entity<ChatMember>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Departments>().HasKey(x => x.Id);
            modelBuilder.Entity<Departments>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Departments>().HasMany(x => x.Users).WithOne().HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<UserEvents>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEvents>().Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}
