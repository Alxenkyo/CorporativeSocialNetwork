﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class MessageStatusDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
