using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class UserTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Users> Users { get; set; }
    }
}
