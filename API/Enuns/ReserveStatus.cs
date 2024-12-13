using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Enuns
{
    public enum ReserveStatus
    {
        [Display(Name = "Ativo")]
        active,
        [Display(Name = "Cancelado")]
        canceled
    }
}