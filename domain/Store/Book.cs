using Store.Data;
using System;
using System.Text.RegularExpressions;

namespace Store
{
    public class Book
    {
        private readonly BookDto _dto;

        public int Id => _dto.Id;

        public string Isbn
        {
            get => _dto.Isbn;
            set => _dto.Isbn = value;
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

        public string Description 
        {
            get => _dto.Description;
            set => _dto.Description = value;
        }

        public decimal Price 
        {
            get => _dto.Price;
            set => _dto.Price = value;
        }

        internal Book(BookDto dto)
        {
            _dto = dto;
        }
        
        public static bool TryFormatIsbn(string isbn, out string formattedIsbn)
        {
            if (isbn == null)
            {
                formattedIsbn = null;
                return false;
            }

            formattedIsbn = isbn.Replace("-", "")
                                .Replace(" ", "")
                                .ToUpper();

            return Regex.IsMatch(formattedIsbn, @"^ISBN\d{10}(\d{3})?&");
        }

        public static bool IsIsbn(string s) => TryFormatIsbn(s, out string formattedIsbn);
    }
}
