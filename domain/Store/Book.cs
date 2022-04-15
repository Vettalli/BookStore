using System.Text.RegularExpressions;
using Store.Data;
using System;

namespace Store
{
    public class Book
    {
        private readonly BookDto _dto;

        public int Id => _dto.Id;

        public string Isbn 
        {
            get => _dto.Isbn;
            set 
            {
                if (TryIsbnFormat(value, out string formattedIsbn))
                {
                    _dto.Isbn = formattedIsbn;
                }

                throw new ArgumentException(nameof(Isbn));
            }
        }

        public string Author 
        {
            get => _dto.Author;
            set => _dto.Author = value?.Trim();
        }

        public string Title 
        {
            get => _dto.Title;
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Title));
                }

                _dto.Title = value.Trim();
            }
        }

        public string Description { get; }

        public decimal Price { get; }

        public Book(BookDto dto)
        {
            _dto = dto;
        }

        public static bool TryIsbnFormat(string isbn, out string formattedIsbn)
        {
            if(isbn == null)
            {
                formattedIsbn = null;
                return false;
            }

            formattedIsbn = isbn.Replace("-", "")
                                .Replace(" ", "")
                                .ToUpper();

            return Regex.IsMatch(formattedIsbn, @"^ISBN\d{10}(\d{3})?$");
        }
        
        public static bool IsIsbn(string isbn) => TryIsbnFormat(isbn, out string _);

        public static class DtoFactory
        {
            public static BookDto Create(string isbn,
                                         string author,
                                         string title,
                                         string description,
                                         decimal price)
            {
                if(TryIsbnFormat(isbn, out string formattedIsbn))
                {
                    isbn = formattedIsbn;
                }
                else
                {
                    throw new ArgumentException(nameof(isbn));
                }

                if (string.IsNullOrEmpty(title))
                {
                    throw new ArgumentException(nameof(title));
                }

                return new BookDto
                {
                    Isbn = isbn,
                    Author = author?.Trim(),
                    Title = title.Trim(),
                    Description = description?.Trim(),
                    Price = price
                };
            }
        }

        public static class Mapper 
        {
            public static Book Map(BookDto dto) => new Book(dto);

            public static BookDto Map(Book domain) => domain._dto;
        }
    }
}
