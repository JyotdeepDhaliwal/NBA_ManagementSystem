using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Jersey Number is required")]
        [Range(0, 99, ErrorMessage = "Jersey Number must be between 0 and 99")]
        public int JerseyNumber { get; set; }

        [Required(ErrorMessage = "Player Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Height is required")]
        public string Height { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Team")]
        [Required(ErrorMessage = "Team selection is required")]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}