using System.Collections.Generic;
using System.Text;
using System;
using Xunit;

namespace BookStore.Test
{
    public class OrderTests
    {
        [Fact]
        public void Order_WithNullItems_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(()=> 
            {
                new Order(1, null);
            });
        }

        [Fact]
        public void TotalAmount_WithNonEmptyItemsList_CalculateTotalAmount()
        {
            var order = new Order(1, new List<OrderItem> 
            {
                new OrderItem(1, 5, 0m),
                new OrderItem(2,5,0m)
            });

            Assert.Equal(10, order.TotalAmount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItemsList_CalculateTotalPrice ()
        {
            var order = new Order(1, new List<OrderItem>
            {
                new OrderItem(1, 5, 50m),
                new OrderItem(2,10, 10m)
            });

            Assert.Equal(350m, order.TotalPrice);
        }
    }
}
