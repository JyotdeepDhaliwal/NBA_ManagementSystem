using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        public string Telephone { get; set; }
        public string Owner { get; set; }
        public string HeadCoach { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}