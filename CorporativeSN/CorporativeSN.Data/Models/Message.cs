using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat MChat { get; set; }
        public int UserId { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusId { get; set; }
        public MessageStatus Status { get; set; }
    }
}
