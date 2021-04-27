using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<EventDTO> CreatedEvents { get; set; }
        public IEnumerable<ChatMemberDTO> ChatMembers { get; set; }
        public IEnumerable<ChatDTO> CreatedChats { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
