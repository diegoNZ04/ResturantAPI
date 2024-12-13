using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Enuns
{
    public enum TableStatus
    {
        [Display(Name = "Disponível")]
        available,
        [Display(Name = "Reservado")]
        reserved,
        [Display(Name = "Inativo")]
        inactive
    }
}