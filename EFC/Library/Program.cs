using System;
using System.Linq;
using Library.Models;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            // AddBookGenreAuthor();
            // AddAuthor();
            // AddBookToAuthor();
            // GetBook();
            GetBookAndAll();
        }

        private static void GetBookAndAll()
        {
            using (LibraryContext lb = new LibraryContext())
            {
                Book first = lb.Books.
                    Include(book => book.Author).
                    Include(book => book.Genre).
                    First(book => book.BookId == 765326353);
                Console.WriteLine(first);
            }
        }

        private static void GetBook()
        {
            using (LibraryContext lb = new LibraryContext())
            {
                Book first = lb.Books.Where(book => book.BookId == 765326353).First();
                Console.WriteLine(first);
            }
        }

        private static void AddBookToAuthor()
        {
            using (LibraryContext lb = new LibraryContext())
            {
                Author a = lb.Authors.First(a => a.FirstName.Equals("Stephen") && a.LastName.Equals("King"));

                Book b = new Book
                {
                    Author = a,
                    Title = "The Shining",
                    BookId = 0450040186,
                    PublishDate = new DateTime(1980, 1, 28),
                    TotalPages = 659
                };

                lb.Books.Add(b);
                lb.SaveChanges();
            }
        }

        private static void AddAuthor()
        {
            Author a = new Author
            {
                FirstName = "Stephen",
                LastName = "King",
                Bio = "I am pretty famous"
            };
            using (LibraryContext lb = new LibraryContext())
            {
                lb.Authors.Add(a);
                lb.SaveChanges();
            }
        }

        private static void AddBookGenreAuthor()
        {
            Genre g = new Genre
            {
                GenreName = "High Fantasy"
            };
            Author a = new Author
            {
                FirstName = "Brandon",
                LastName = "Sanderson",
                Bio = "I write good stuff"
            };
            Book b = new Book
            {
                Author = a,
                Genre = g,
                Title = "The Way of Kings",
                BookId = 0765326353,
                PublishDate = new DateTime(2010, 7, 31),
                TotalPages = 1007
            };
            using (LibraryContext lb = new LibraryContext())
            {
                lb.Books.Add(b);
                lb.SaveChanges();
            }
        }
    }
}