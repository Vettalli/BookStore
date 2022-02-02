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

        List<Book> comics = new List<Book>();

        public List<Book> GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                             join bookId in bookIds on book.Id equals bookId
                             select book;

            var testFoundBooks = books.Where(book => bookIds.Contains(book.Id)).ToList();

            return foundBooks.ToList();
        }

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

            foundBooks = books.Where(book => book.Author.ToLower().Contains(titleAndAuthor.ToLower()) 
                                          || book.Title.ToLower().Contains(titleAndAuthor.ToLower()))
                                                       .ToList();

            return foundBooks;
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
