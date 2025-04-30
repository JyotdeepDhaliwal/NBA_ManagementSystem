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

        [Required(ErrorMessage = "Team name is required")]
        [StringLength(50, ErrorMessage = "Team name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters")]
        public string City { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid telephone number. It must be exactly 10 digits.")]
        public string Telephone { get; set; }

        [StringLength(50, ErrorMessage = "Owner name cannot exceed 50 characters")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Head Coach name is required")]
        [StringLength(50, ErrorMessage = "Head Coach name cannot exceed 50 characters")]
        public string HeadCoach { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Games Won cannot be negative")]
        public int GamesWon { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Games Lost cannot be negative")]
        public int GamesLost { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
