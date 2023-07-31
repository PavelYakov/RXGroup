namespace RXApp.Model
{
    public class BookRequest
    {
        public string Title { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Author { get; set; }
        public string Article { get; set; }
        public int CreateDate { get; set; }
        public int? Copies { get; set; }
    }
}
