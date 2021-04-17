using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Models
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserDTO> Members { get; set; }
    }
}
