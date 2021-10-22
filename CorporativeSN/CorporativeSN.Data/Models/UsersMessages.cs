using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Data.Models
{
    public class UsersMessages
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int CreatorId { get; set; }
        public virtual Users User { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public List<MessagesAttachments> MessagesAttachments { get; set; }
    }
}
