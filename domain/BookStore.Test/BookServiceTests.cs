using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace BookStore.Test
{
    public class BookServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>())).Returns(new List<Book> { new Book(1,"","","")});
            
            var bookService = new BookService(bookRepositoryStub.Object);
            var isbn = "ISBN 12345-12345";

            var actual = bookService.GetAllByQuery(isbn);

            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
        }



        [Fact]
        public void GetAllByQuery_WithTitleOrAuthor_CallsGetByTitleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByTitleAndAuthor(It.IsAny<string>())).Returns(new List<Book> { new Book(2,"","","")});

            var bookService = new BookService(bookRepositoryStub.Object);
            var query = "egre";

            var actual = bookService.GetAllByQuery(query);

            Assert.Collection(actual, book => Assert.Equal(2, book.Id));
        }
    }
}
