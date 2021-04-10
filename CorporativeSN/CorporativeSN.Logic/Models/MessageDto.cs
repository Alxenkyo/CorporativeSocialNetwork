using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
