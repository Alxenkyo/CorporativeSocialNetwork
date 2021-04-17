using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CorporativeSN.Logic.Models
{
    public class EventDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CreatorId { get; set; }
        //public User Creator { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
