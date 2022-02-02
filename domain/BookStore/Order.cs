using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items { get => _items; }

        public int TotalAmount
        {
            get => _items.Sum(item => item.Count);
        }

        public decimal TotalPrice 
        {
            get => _items.Sum(item => item.Price * item.Count);
        }

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Id = id;
            _items = new List<OrderItem>(items);
        }

        public OrderItem GetItem(int bookId)
        {
            var index = _items.FindIndex(i => i.BookId == bookId);
            
            if(index == -1)
            {
                ThrowBookException("Book not found", bookId);
            }

            return _items[index];
        }

        public void AddOrUpdateItem(Book book, int count)
        {
            ThrowBookNullException(book);

            var index = _items.FindIndex(i => i.BookId == book.Id);

            if (index == -1)
            {
                _items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else
            {
                _items[index].Count += count;
            }            
        }

        public void RemoveItem(int bookId)
        {
            var index = _items.FindIndex(i => i.BookId == bookId);

            if (index == -1)
            {
                ThrowBookException("Order doesn't contain specified item", bookId);
            }

            _items.RemoveAt(index);
        }

        public void AddBook(Book book)
        {
            ThrowBookNullException(book);

            AddOrUpdateItem(book, 1);
        }

        public void RemoveBook(Book book)
        {
            ThrowBookNullException(book);

            AddOrUpdateItem(book, -1); 
        }             

        private void ThrowBookException(string message, int bookId)
        {
            var exception = new InvalidOperationException(message);

            exception.Data["BookId"] = bookId;

            throw exception;
        }

        private void ThrowBookNullException(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
        }
    }
}