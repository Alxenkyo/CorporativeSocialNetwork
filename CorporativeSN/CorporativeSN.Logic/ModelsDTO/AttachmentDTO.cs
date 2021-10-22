using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CorporativeSN.Logic.Models
{
    public class AttachmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MessageId { get; set; }
        public string BinaryData { get; set; }

    }
}
