using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RXApp.Model;
using RXApp.Services;

namespace RXApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        // Добавление нового читателя
        [HttpPost]
        public IActionResult AddReader([FromBody] ReaderRequest readerRequest)
        {
            var reader = new Reader
            {
                FirstName = readerRequest.FirstName,
                FatherName = readerRequest.FatherName,
                LastName = readerRequest.LastName,
                AddDate = readerRequest.AddDate,
                UpdateDate = readerRequest.UpdateDate,
                BornDate = readerRequest.BornDate
            };

            try
            {
                var addedReader = _readerService.AddReader(reader);
                return CreatedAtAction(nameof(GetReaderById), new { id = addedReader.Id }, addedReader);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // Измененние данных о читателе
        [HttpPut("{id}")]
        public IActionResult UpdateReader(int id, Reader reader)
        {
            var success = _readerService.UpdateReader(id, reader);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        //Удаление данных о читателе
        [HttpDelete("{id}")]
        public IActionResult DeleteReader(int id)
        {
            var success = _readerService.DeleteReader(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        //Выдать книгу читателю
        [HttpPost("{readerId}/books/{bookId}/issue")]
        public IActionResult IssueBookToReader(int readerId, int bookId)
        {
            var success = _readerService.IssueBook(readerId, bookId);
            if (success)
            {
                return Ok();
            }

            return Conflict("Книга уже выдана данному читателю или читатель/книга не найдены.");
        }

        // Возврат книги
        [HttpPost("{readerId}/books/{bookId}/return")]
        public IActionResult ReturnBookFromReader(int readerId, int bookId)
        {
            var success = _readerService.ReturnBook(readerId, bookId);
            if (success)
            {
                return Ok();
            }

            return NotFound("Книга не была выдана данному читателю.");
        }

        // Данные о читателе со списком выданных ему книг
        [HttpGet("{id}")]
        public IActionResult GetReaderById(int id)
        {
            var reader = _readerService.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        // Поиск читателя по ФИО
        [HttpGet("search")]
        public IActionResult SearchReaders(string query)
        {
            var matchedReaders = _readerService.SearchReaders(query);
            return Ok(matchedReaders);
        }

        // История взятия/сдачи книги по читателю
        [HttpGet("{readerId}/history")]
        public IActionResult GetReaderHistory(int readerId)
        {
            var historyEntries = _readerService.GetReaderHistory(readerId);
            return Ok(historyEntries);
        }
    }
}
