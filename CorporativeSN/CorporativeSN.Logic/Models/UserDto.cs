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
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<EventDTO> CreatedEvents { get; set; }
        public IEnumerable<ChatMemberDTO> ChatMembers { get; set; }
    }
}
