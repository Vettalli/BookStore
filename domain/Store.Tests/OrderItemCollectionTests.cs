using Store.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace Store.Tests
{
    public class OrderItemCollectionTests
    {
        private static Order CreateTestOrder()
        {
            return new Order
                (new OrderDto
                {
                    Id = 1,
                    Items = new List<OrderItemDto>
                    {
                        new OrderItemDto{ BookId = 1, Count = 3, Price = 10m},
                        new OrderItemDto{ BookId = 2, Count = 2, Price = 10m}
                    }
                });
        }

        [Fact]
        public void Get_WithExistingItem_ReturnsItem()
        {
            var order = CreateTestOrder();

            var orderItem = order.Items.Get(1);

            Assert.Equal(3, orderItem.Count);
        }

        [Fact]
        public void Get_WithNonExistingItem_ThrowsInvalidOperationException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Get(100);
            });
        }

        [Fact]
        public void Add_WithExistingItem_ThrowInvalidOperationException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Add(1, 10m, 10);
            });
        }
       
        [Fact]
        public void Remove_WithNonExistingItem_ThrowsInvalidOperationException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Remove(100);
            });
        }
    }
}
