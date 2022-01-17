using System.Linq;
using System.Collections.Generic;

namespace BookStore.Memory
{
    public class BookRepository : IBookRepository
    {
        List<Book> books = new List<Book>
        {
            new Book(1, "Andrew W. Troelsen", "c# programming book")
        };

        public List<Book> GetAllByTitle(string title)
        {
            return books.Where(book => book.Title.Contains(title)).ToList();
        }
    }
}
