using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RXApp.Model;

namespace RXApp.Services
{
    public class ReaderService: IReaderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReaderService> _logger;
        public ReaderService(ApplicationDbContext context, ILogger<ReaderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Reader AddReader(Reader reader)
        {
           
            // Проверка на уникальность ФИО и даты рождения
            if (_context.Readers.Any(r => r.FirstName == reader.FirstName && r.FatherName == reader.FatherName && r.LastName == reader.LastName && r.BornDate == reader.BornDate))
            {
                _logger.LogWarning("Читатель с таким ФИО   и датой рождения уже существует.");
                throw new InvalidOperationException("Читатель с таким ФИО и датой рождения уже существует.");
            }

            reader.AddDate = DateTime.Now;
            _context.Readers.Add(reader);
            _context.SaveChanges();
            return reader;
        }

        public bool UpdateReader(int id, Reader reader)
        {
            var existingReader = _context.Readers.Find(id);
            if (existingReader == null)
            {
                _logger.LogWarning("Читатель с таким ID   не существует.");
                return false;
            }

            existingReader.FirstName = reader.FirstName;
            existingReader.FatherName = reader.FatherName;
            existingReader.LastName = reader.LastName;
            existingReader.BornDate = reader.BornDate;

            reader.UpdateDate = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteReader(int id)
        {
            var reader = _context.Readers.Find(id);
            if (reader == null)
            {
                _logger.LogWarning("Читатель с таким ID   не существует.");
                return false;
            }

            // Проверка на выдачу книги
            if (_context.Issues.Any(issue => issue.ReaderId == id && issue.DateReturned == null))
            {
                _logger.LogWarning("Читатель имеет выданные книги и не может быть удален.");
                return false;
            }

            _context.Readers.Remove(reader);
            _context.SaveChanges();
            return true;
        }

        public bool IssueBook(int readerId, int bookId)
        {
            var reader = _context.Readers.Find(readerId);
            var book = _context.Books.Find(bookId);

            if (reader == null || book == null)
            {
                _logger.LogWarning("Читателя или книги не существует.");
                return false;
            }

            // Проверка на доступность книги
            if (_context.Issues.Any(issue => issue.BookId == bookId && issue.DateReturned == null))
            {
                _logger.LogWarning("Книга уже выдана другому читателю");
                return false;
            }

            var issue = new Issue
            {
                ReaderId = readerId,
                BookId = bookId,
                DateIssued = DateTime.Now
            };

            _context.Issues.Add(issue);
            _context.SaveChanges();
            return true;
        }

        public bool ReturnBook(int readerId, int bookId)
        {
            var issue = _context.Issues.FirstOrDefault(issue => issue.ReaderId == readerId && issue.BookId == bookId && issue.DateReturned == null);
            if (issue == null)
            {
                _logger.LogWarning("У читателя нет такой выданной книги.");
                return false;
            }

            issue.DateReturned = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public Reader GetReaderById(int id)
        {
            return _context.Readers.Find(id);
           
        }

        public List<Reader> SearchReaders(string query)
        {
            return _context.Readers
                .Include(r => r.Issues)
                .ThenInclude(issue => issue.Book)
                .Where(reader => (reader.FirstName + " " + reader.FatherName + " " + reader.LastName)
                    .Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();


        }

        public List<History> GetReaderHistory(int readerId)
        {
           
            return _context.Issues
                .Where(issue => issue.ReaderId == readerId)
                .Select(issue => new History
                {
                    Id = issue.Id,
                    ReaderId = issue.ReaderId,
                    BookId = issue.BookId,
                    DateIssued = issue.DateIssued,
                    DateReturned = issue.DateReturned
                })
                .ToList();

        }
    }
}
