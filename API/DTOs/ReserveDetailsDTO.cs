using RestaurantAPI.Enuns;

namespace RestaurantAPI.DTOs
{
    public class ReserveDetailsDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public int PeopleNumber { get; set; }
        public DateTime ReserveDate {get; set;}
        public ReserveStatus Status { get; set; }

    }
}