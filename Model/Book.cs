namespace RXApp.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Author { get; set; }
        public string Article { get; set; }
        public int CreateDate { get; set; } // только год
        public int? Copies { get; set; } // количество экз.

        //список связанных выдач книг
        public  ICollection<Issue> Issues { get; set; }
    }
}
