
using RestaurantAPI.Domain.Enums;

namespace RestaurantAPI.Domain.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
    }
}