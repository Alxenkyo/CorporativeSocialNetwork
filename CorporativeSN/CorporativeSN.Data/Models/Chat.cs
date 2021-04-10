using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChatMember> Members { get; set; }
    }
}
