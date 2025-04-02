using RestaurantAPI.Enuns;

namespace RestaurantAPI.DTOs
{
    public class TableDTO
    {
        // Name
        public string Name { get; set; }
        // Capacity
        public int Capacity { get; set; }
        // Status
        public TableStatus Status { get; set; }
    }
}