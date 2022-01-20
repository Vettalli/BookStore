using System.Linq;
using System.Collections.Generic;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        List<Book> books = new List<Book>
        {
            new Book(1, "ISBN 12434-43532","Andrew W. Troelsen", "c# programming book"),
            new Book(2, "ISBN 12434-96435","Rybakov", "Arbats' children")
        };

        public List<Book> GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToList();
        }

        public List<Book> GetAllByTitleAndAuthor(string titleAndAuthor)
        {
            var foundBooks = new List<Book>();

            if (titleAndAuthor == null)
            {
                return foundBooks;
            }

            foundBooks = books.Where(book => book.Author.Contains(titleAndAuthor) || book.Title.Contains(titleAndAuthor)).ToList();

            return foundBooks;
        }
    }
}
