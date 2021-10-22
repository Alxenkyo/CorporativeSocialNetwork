using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int UserTypeId { get; set; }
        public byte[] ImageData { get; set; }
        public virtual UserTypes UserType { get; set; }
        public int DepartmentId { get; set; }
        public virtual Departments Department { get; set; }
        public List<ChatMember> ChatMembers { get; set; }
        public List<Chats> CreatedChats { get; set; }
        public List<UsersMessages> Messages { get; set; }
    }
}
