using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    [Table("UserTeamPlayers")]
    public class UserTeamPlayer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserTeam")]
        public int UserTeamId { get; set; }

        [Required]
        public string PlayerName { get; set; }

        public string Position { get; set; }
        public string Height { get; set; }

        public virtual UserTeam UserTeam { get; set; }
    }
}