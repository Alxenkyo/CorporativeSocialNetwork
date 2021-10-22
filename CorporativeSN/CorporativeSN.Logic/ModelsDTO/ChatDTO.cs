using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class ChatDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int CreatorId { get; set; }
        public string ChatType { get; set; }
        public IEnumerable<int> MembersId { get; set; }
        public IEnumerable<ChatMemberDTO> Members { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
