using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusId { get; set; }
    }
}
