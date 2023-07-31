using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RXApp.Model;
using Microsoft.Extensions.Logging;
using RXApp.Controllers;
using System.Net;

namespace RXApp.Services
{
    public class BookService: IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(ApplicationDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Book AddBook(Book book)
        {
            // Проверка на уникальность артикула
            if (_context.Books.Any(b => b.Article == book.Article))
            {
               _logger.LogWarning("Книга с таким артикулом уже существует.");
               return null;
            }

            book.AddDate = DateTime.Now;
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public bool UpdateBook(int id , Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                _logger.LogWarning("Книга с таким ID не существует.");
                return false;
            }

            // Проверка на выдачу книги
            if (_context.Issues.Any(issue => issue.BookId == id))
            {
                _logger.LogWarning("Книга уже выдана и не может быть изменена.");
                return false; 
            }
            book.UpdateDate = DateTime.Now;
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;


            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                _logger.LogWarning("Книга с таким ID не найдена.");
                return false;
            }

            // Проверка на выдачу книги
            if (_context.Issues.Any(issue => issue.BookId == bookId))
            {
                _logger.LogWarning("Книга уже выдана и не может быть удалена.");
                return false; 
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public Book GetBookById(int bookId)
        {
            var book = _context.Books
                .Include(b => b.Issues)
                .ThenInclude(issue => issue.Reader)
                .FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                _logger.LogWarning("Книга с таким ID не найдена.");
            }

            return book;
        }

        public List<Book> GetAllBooks()
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        public List<Book> GetIssuedBooks()
        {
            return _context.Books
                .Where(book => _context.Issues.Any(issue => issue.BookId == book.Id && issue.DateReturned == null))
                .ToList();
           
        }

        public List<Book> GetAvailableBooks()
        {
            return _context.Books
                .Where(book => !_context.Issues.Any(issue => issue.BookId == book.Id && issue.DateReturned == null))
                .ToList();

            
        }

        public List<Book> SearchBooks(string query)
        {
            return _context.Books
                .Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

           
        }

        public List<History> GetBookHistory(int bookId)
        {
           
            var history = _context.Issues
                .Where(issue => issue.BookId == bookId)
                .Select(issue => new History
                {
                    ReaderId = issue.ReaderId,
                    DateIssued = issue.DateIssued,
                    DateReturned = issue.DateReturned
                })
                .ToList();

            return history;
        }
    }
}
