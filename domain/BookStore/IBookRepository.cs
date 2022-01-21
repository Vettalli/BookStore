using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public interface IBookRepository
    {
        List<Book> GetAllByIsbn(string isbn);
        List<Book> GetAllByTitleAndAuthor(string titleAndAuthor);
        Book GetById(int id);
    }
}
