using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CorporativeSN.Logic.Models
{
    public class EventDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CreatorId { get; set; }
    }
}
