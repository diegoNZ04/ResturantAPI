
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public int ReserveId { get; set; }
        [ForeignKey("ReserveId")]
        public Reserve Reserve { get; set; } = null!;
    }
}