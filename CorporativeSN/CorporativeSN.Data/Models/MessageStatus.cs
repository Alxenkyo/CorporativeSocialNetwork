using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class MessageStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<UsersMessages> Messages { get; set; }
    }
}
