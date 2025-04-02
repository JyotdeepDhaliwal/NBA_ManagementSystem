using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    [Table("Trades")]
    public class Trade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("TeamFrom")]
        public int TeamFromId { get; set; }

        [ForeignKey("TeamTo")]
        public int TeamToId { get; set; }

        public string Contracts { get; set; }
        public DateTime TradeDate { get; set; }

        public virtual Team TeamFrom { get; set; }
        public virtual Team TeamTo { get; set; }
    }
}