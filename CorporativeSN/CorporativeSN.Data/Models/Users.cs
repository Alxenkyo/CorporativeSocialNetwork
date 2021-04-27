using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public List<UserEvents> CreatedEvents { get; set; }
        public List<ChatMember> ChatMembers { get; set; }
        public List<Chats> CreatedChats { get; set; }
        public List<UsersMessages> Messages { get; set; }
    }
}
