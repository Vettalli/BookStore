using System;
using Xunit;

namespace BookStore.Test
{
    public class BookTests
    {        
        [Fact]
        public void IsIsbn_WithNull_ReturnFalse()
        {
            bool actual = Book.IsIsbn(null);

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithSpaces_ReturnFalse()
        {
            bool actual = Book.IsIsbn("  ");
            
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidInput_ReturnFalse()
        {
            bool actual = Book.IsIsbn("124265");

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithCorrectInput_ReturnTrue()
        {
            bool actual = Book.IsIsbn("ISBN 11111-11111");

            Assert.True(actual);
        }
    }
}
