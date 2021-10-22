using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class Chats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public string ChatType { get; set; }
        public List<ChatMember> Members { get; set; }
        public List<UsersMessages> Messages { get; set; }
    }
}
