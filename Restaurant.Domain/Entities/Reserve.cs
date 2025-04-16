using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities;

public class Reserve
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TableNumber { get; set; }
    public int PeopleNumber { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime ReserveDate { get; set; }
    public ReserveStatus Status { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    public int TableId { get; set; }
    public Table Table { get; set; } = null!;
}