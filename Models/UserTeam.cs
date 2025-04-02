using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    [Table("UserTeams")]
    public class UserTeam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TeamName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<UserTeamPlayer> Players { get; set; }
    }
}