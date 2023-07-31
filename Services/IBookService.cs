using RXApp.Model;

namespace RXApp.Services
{
    public interface IBookService
    {
        Book AddBook(Book book);
        bool UpdateBook(int id, Book book);
        bool DeleteBook(int bookId);
        Book GetBookById(int bookId);
        List<Book> GetAllBooks();
        List<Book> GetIssuedBooks();
        List<Book> GetAvailableBooks();
        List<Book> SearchBooks(string query);

        List<History> GetBookHistory(int bookId);
    }
}
