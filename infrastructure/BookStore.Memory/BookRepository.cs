using System.Linq;
using System.Collections.Generic;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        List<Book> books = new List<Book>
        {
            new Book(1, "Andrew W. Troelsen", "c# programming book"),
            new Book(2, "Rybakov", "Arbats' children")
        };

        public List<Book> GetAllByTitle(string title)
        {
            List<Book> foundBooks = new List<Book>(books);

            if (title != null)
            {
                foundBooks = books.Where(book => book.Title.Contains(title)).ToList(); 
            }

            return foundBooks;
        }
    }
}
