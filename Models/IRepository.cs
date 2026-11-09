using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        List<Book> GetByAuthor(string author);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }


    public class MockDatabase : IRepository
    {
        private readonly List<Book> _books = new()
        {
            new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", PublishedYear = 2008 },
            new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", PublishedYear = 1999 }
        };

        public List<Book> GetAll()
        {
            return _books.ToList();
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetByAuthor(string author)
        {
            return _books
                .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Add(Book book)
        {
            // Automatikus ID generálás szimulálása
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
        }

        public void Update(Book book)
        {
            var existing = GetById(book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.Author = book.Author;
                existing.PublishedYear = book.PublishedYear;
                existing.IsAvailable = book.IsAvailable;
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }


    }
}
