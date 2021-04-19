using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class UserTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
