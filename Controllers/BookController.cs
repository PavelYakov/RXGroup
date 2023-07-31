using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RXApp.Model;
using RXApp.Services;
using System.Reflection.PortableExecutable;

namespace RXApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Добавление новой книги
        [HttpPost]
        public IActionResult AddBook([FromBody] BookRequest bookRequest)
        {
            
            Book book = new Book
            {
                Title = bookRequest.Title,
                AddDate = bookRequest.AddDate,
                UpdateDate = bookRequest.UpdateDate,
                Author = bookRequest.Author,
                Article = bookRequest.Article,
                CreateDate = bookRequest.CreateDate,
                Copies = bookRequest.Copies
            };

            var addedBook = _bookService.AddBook(book);
            if (addedBook == null)
            {
                return BadRequest("Книга с таким артикулом уже существует.");
            }
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        
        }

        //Измненение данных о книге
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var updated = _bookService.UpdateBook(id, book);
            if (updated)
            {
                return Ok();
            }
            return NotFound("Книга не найдена или уже выдана.");

        }

        // Удаление книги
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleted = _bookService.DeleteBook(id);
            if (deleted)
            {
                return Ok();
            }
            return NotFound("Книга не найдена или уже выдана.");
        }

        //Получение данных о книге
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Книга не найдена.");
            }
            return Ok(book);
        }

        // Получение общего списка книг
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var allBooks = _bookService.GetAllBooks();
            return Ok(allBooks);
        }

        // Получение списка выданных книг с указанием читателей
        [HttpGet("issued")]
        public IActionResult GetIssuedBooks()
        {
            var issuedBooks = _bookService.GetIssuedBooks();
            return Ok(issuedBooks);
        }

        // Получение списка доступных для выдачи книг
        [HttpGet("available")]
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _bookService.GetAvailableBooks();
            return Ok(availableBooks);
        }

        // Поиск книг по части наименования
        [HttpGet("search")]
        public IActionResult SearchBooks(string query)
        {
            var matchedBooks = _bookService.SearchBooks(query);
            return Ok(matchedBooks);
        }

        //История взятия/сдачи книги читателями
       [HttpGet("{bookId}/history")]
        public IActionResult GetBookHistory(int bookId)
        {
            var history = _bookService.GetBookHistory(bookId);
            return Ok(history);
        }
    }
}
