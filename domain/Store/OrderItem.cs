using Store.Data;
using System;

namespace Store
{
    public class OrderItem
    {
        private readonly OrderItemDto _dto;

        public int BookId => _dto.BookId;

        public int Count
        {
            get { return _dto.Count; }
            set
            {
                //ThrowIfInvalidCount(value);

                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Count must be greater than zero.");

                _dto.Count = value;
            }
        }

        public decimal Price 
        {
            get => _dto.Price;
            set => _dto.Price = value;
        }

        internal OrderItem(OrderItemDto dto)
        {
            _dto = dto;
        }

        private static void ThrowIfInvalidCount(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");
        }

        public static class DtoFactory
        {
            public static OrderItemDto Create(OrderDto order, int bookId, decimal price, int count)
            {
                if(order == null)
                {
                    throw new ArgumentNullException(nameof(order));
                }

                return new OrderItemDto
                {
                    Id = order.Id,
                    BookId = bookId,
                    Count = count,
                    Order = order,
                    Price = price
                };
            }
        }

        public static class Mapper
        {
            public static OrderItem Map(OrderItemDto dto) => new OrderItem(dto);

            public static OrderItemDto Map(OrderItem domain) => domain._dto;
        }
    }
}
