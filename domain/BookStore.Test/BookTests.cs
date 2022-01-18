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
        public void IsIsbn_WithInvalidIsbn_ReturnFalse()
        {
            bool actual = Book.IsIsbn("1245");

            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidIsbn_ReturnTure()
        {
            bool actual = Book.IsIsbn("ISBN 12434-43532");

            Assert.True(actual);
        }
    }
}
