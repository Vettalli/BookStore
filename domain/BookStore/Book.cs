using System;

namespace BookStore
{
    public class Book
    {
        public int Id { get; }
        public string Author { get; }
        public string Title { get; }

        public Book()
        {}

        public Book(int id, string author, string title)
        {
            Id = id;
            Author = author;
            Title = title;
        }
    }
}
