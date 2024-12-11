
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantAPI.Enuns;

namespace RestaurantAPI.Entities
{
    public class Table
    {
        // Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // Name
        [Required(ErrorMessage = "O nome ou número da mesa é obrigatório.")]
        [StringLength(100, ErrorMessage =
            "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        // Capacity
        [Required(ErrorMessage = "A capacidade da mesa é obrigatória")]
        [Range(1, 12)]
        public int Capacity { get; set; }
        // Status
        [Required(ErrorMessage = "O status da mesa é obrigatório")]
        public TableStatus Status { get; set; }
    }
}