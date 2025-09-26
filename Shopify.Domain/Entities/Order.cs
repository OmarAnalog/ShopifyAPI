using Shopify.Domain.Enums;

namespace Shopify.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; } = Status.Pending;
    }
}
