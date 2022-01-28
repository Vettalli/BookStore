using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Test
{
    public class OrderItemTests
    {       
       [Fact]
       public void OrderItem_WithZeroCount_ThrowArgumentOutOfRangeException()
       {
            Assert.Throws<ArgumentOutOfRangeException>(()=>
            {
                int count = 0;
                new OrderItem(1, count, 0m);
            });
       }

        [Fact]
        public void OrderItem_WithNegativeCount_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=> 
            {
                int count = -1;
                new OrderItem(1, count, 0m);
            });
        }        
    }
}
