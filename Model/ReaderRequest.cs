namespace RXApp.Model
{
    public class ReaderRequest
    {
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime BornDate { get; set; }
    }
}
