using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class BookService
    {
        private IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllByQuery(string query)
        {
            if (Book.IsIsbn(query))
            {
                return _bookRepository.GetAllByIsbn(query);
            }

            return _bookRepository.GetAllByTitleAndAuthor(query);
        }
    }
}
