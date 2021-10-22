using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data.Models
{
    public class Documents
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Departments Department { get; set; }
    }
}
