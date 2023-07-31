namespace RXApp.Model
{
    public class History
    {
        public int Id { get; set; } // Ид  записи
        public int ReaderId { get; set; } // Ид читателя
        public int BookId { get; set; } // Ид книги
        public int IssueId { get; set; } // Ид выдачи


        public DateTime DateIssued { get; set; } // Дата выдачи книги читателю
        public DateTime? DateReturned { get; set; } // Дата возврата книги в библиотеку 


        public Book Book { get; set; } // Ссылка на книгу
        public Reader Reader { get; set; } // Ссылка на читателя
        public Issue Issue { get; set; } // Ссылка на выдачу
    }
}
