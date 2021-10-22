using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Models
{
    public class UsersListDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string DepartmentName { get; set; }
        public string ImageData { get; set; }
    }
}
