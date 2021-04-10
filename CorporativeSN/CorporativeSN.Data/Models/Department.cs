using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Members { get; set; }
    }
}
