using System.Collections.Generic;

namespace BookStore.Web.Models
{
    public class Cart
    {
        public Dictionary<int, int> Items { get; set; } = new Dictionary<int, int>();

        public decimal SumCost { get; set; }
    }
}
