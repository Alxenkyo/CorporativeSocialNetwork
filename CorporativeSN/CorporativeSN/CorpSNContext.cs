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
        public CorpSNContext(DbContextOptions options) : base(options){}
        public DbSet<Chats> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set ; }
        public DbSet<UsersMessages> UsersMessages { get; set ; }
        public DbSet<Users> Users { get; set ; }
        public DbSet<UserTypes> UserTypes { get ; set ; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<MessagesAttachments> MessagesAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessagesAttachments>().HasKey(x => x.Id);
            modelBuilder.Entity<MessagesAttachments>().HasOne(x => x.Message)
                .WithMany(x => x.MessagesAttachments).HasForeignKey(x => x.MessageId);
            modelBuilder.Entity<Users>().HasKey(x => x.Id);
            modelBuilder.Entity<Users>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Users>().HasMany(x => x.CreatedChats)
                .WithOne().HasForeignKey(x => x.CreatorId);
            modelBuilder.Entity<Users>().HasOne(x => x.UserType)
                .WithMany(x => x.Users).HasForeignKey(x => x.UserTypeId);
            modelBuilder.Entity<Users>().HasOne(x => x.Department)
                .WithMany(x => x.Users).HasForeignKey(x => x.DepartmentId);
            modelBuilder.Entity<UserTypes>().HasKey(x => x.Id);
            modelBuilder.Entity<UserTypes>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UsersMessages>().HasKey(x => x.Id);
            modelBuilder.Entity<UsersMessages>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UsersMessages>().HasOne(x => x.User)
                .WithMany(x => x.Messages).HasForeignKey(x => x.CreatorId);
            modelBuilder.Entity<Chats>().HasKey(x => x.Id);
            modelBuilder.Entity<Chats>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Chats>().HasMany(x => x.Messages)
                .WithOne().HasForeignKey(x => x.ChatId);
            modelBuilder.Entity<ChatMember>().HasKey(x => x.Id);
            modelBuilder.Entity<ChatMember>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ChatMember>().HasOne(x => x.User)
                .WithMany(x => x.ChatMembers).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<ChatMember>().HasOne(x => x.Chat)
                .WithMany(x => x.Members).HasForeignKey(x => x.ChatId);
            modelBuilder.Entity<Departments>().HasKey(x => x.Id);
            modelBuilder.Entity<Departments>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Documents>().HasKey(x => x.Id);
            modelBuilder.Entity<Documents>().HasOne(x => x.Department)
                .WithMany(x => x.Docs).HasForeignKey(x => x.DepartmentId);
            modelBuilder.Entity<MessagesAttachments>().HasKey(x => x.Id);
        }
    }
}
