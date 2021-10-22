using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class UserTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
