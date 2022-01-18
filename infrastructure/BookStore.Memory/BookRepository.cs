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
         
        public List<Book> GetAllByTitleAndAuthor(string query)
        {
            List<Book> foundBooks = new List<Book>(books);

            if (query != null)
            {
                foundBooks = books.Where(book => book.Author.Contains(query) 
                          || book.Title.Contains(query))
                            .ToList();
            }

            return foundBooks;
        }
    }
}
