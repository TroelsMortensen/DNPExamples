using System.Collections.Generic;

namespace Predicates.BookExample
{
    public class RunExample
    {
        public void Run()
        {
            List<Book> books = new BookRepository().GetBooks();

            List<Book> cheapBooks = books.FindAll(CheaperThan50Dollars);
           
            List<Book> alsoCheap = books.FindAll(book => book.Price < 50);
        }

        private bool CheaperThan50Dollars(Book obj)
        {
            return obj.Price < 50;
        }
    }
}