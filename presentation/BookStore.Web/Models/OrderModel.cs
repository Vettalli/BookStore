using System.Collections.Generic;

namespace BookStore.Web.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
