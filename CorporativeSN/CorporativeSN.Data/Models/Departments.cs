using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Users> Users { get; set; }
    }
}
