using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantAPI.Enuns;

namespace RestaurantAPI.Entities
{
    public class Reserve
    {
        // Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        // UserId
        [Required]
        public int UserId { get; set; } = default!;
        // TableId
        [Required]
        public int TableId { get; set; } = default!;
        [Required]
        public int PeopleNumber { get; set; } = default!;
        // ReserveDate
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReserveDate { get; set; } = default!;
        // Status
        [Required]
        public ReserveStatus Status { get; set; } = default!;
    }
}