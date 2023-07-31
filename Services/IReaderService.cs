using RXApp.Model;

namespace RXApp.Services
{
    public interface IReaderService
    {
        
        Reader AddReader(Reader reader);
        bool UpdateReader(int id, Reader reader);
        bool DeleteReader(int id);
        bool IssueBook(int readerId, int bookId);
        bool ReturnBook(int readerId, int bookId);
        Reader GetReaderById(int id);
        List<Reader> SearchReaders(string query);
        List<History> GetReaderHistory(int readerId);
    }
}
