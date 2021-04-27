using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int ChatId { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
