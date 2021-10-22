using CorporativeSN.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Logic.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<AttachmentDTO> MessagesAttachments { get; set; }
    }
}
