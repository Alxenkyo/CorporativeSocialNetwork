using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data.Models
{
    public class MessagesAttachments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MessageId { get; set; }
        public virtual UsersMessages Message { get; set; }
        public byte[] BinaryData { get; set; }
    }
}
