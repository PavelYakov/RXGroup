namespace RXApp.Model
{
    // модель выдачи книг
    public class Issue
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime DateIssued { get; set; }//дата выдачи
        public DateTime? DateReturned { get; set; }//дата возврата

        // связанные книга и читатель
        public Book Book { get; set; }
        public Reader Reader { get; set; }

        // список взятия/сдачи книг
        public ICollection<History> Histories { get; set; }
    }
}
