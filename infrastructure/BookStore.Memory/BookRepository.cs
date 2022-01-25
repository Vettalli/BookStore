using System.Linq;
using System.Collections.Generic;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        List<Book> books = new List<Book>
        {
            new Book(1, "ISBN 12434-43532","Andrew W. Troelsen", "c# programming book", "Book about programming", 59.99m),
            new Book(2, "ISBN 12434-96435","Rybakov", "Arbats' children", "Book about hard core life in Sovok", 99.99m)
        };

        public List<Book> GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToList();
        }

        public List<Book> GetAllByTitleAndAuthor(string titleAndAuthor)
        {
            var foundBooks = new List<Book>();
            titleAndAuthor = titleAndAuthor.ToLower();

            if (titleAndAuthor == null)
            {
                return foundBooks;
            }

            foundBooks = books.Where(book => book.Author.ToLower().Contains(titleAndAuthor) 
                                          || book.Title.ToLower().Contains(titleAndAuthor))
                                                 .ToList();

            return foundBooks;
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
