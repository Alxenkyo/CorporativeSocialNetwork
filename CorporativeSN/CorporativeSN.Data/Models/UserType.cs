using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<User> Users { get; set; }
    }
}
