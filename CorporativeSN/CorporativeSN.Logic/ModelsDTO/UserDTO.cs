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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string TypeName { get; set; }
        public int DepartmentId { get; set; }
        public string ImageData { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<ChatMemberDTO> ChatMembers { get; set; }
        public IEnumerable<ChatDTO> CreatedChats { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
