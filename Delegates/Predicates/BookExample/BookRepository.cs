using System.Collections.Generic;

namespace Predicates.BookExample
{
    public class BookRepository
    {
        public List<Book> GetBooks()
        {
            return new()
            {
                new Book() {Title = "First", Price = 42},
                new Book() {Title = "Second", Price = 1337},
                new Book() {Title = "Third", Price = 12.34m},
                new Book() {Title = "Fourth", Price = 65.95m}
            };
        }
    }
}