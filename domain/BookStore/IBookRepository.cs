using System.Collections.Generic;

namespace BookStore
{
    public interface IBookRepository
    {
        List<Book> GetAllByIsbn(string isbn);
        List<Book> GetAllByTitleAndAuthor(string titleAndAuthor);
        Book GetById(int id);
        List<Book> GetAllByIds(IEnumerable<int> bookIds);
    }
}
