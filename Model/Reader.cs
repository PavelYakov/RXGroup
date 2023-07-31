namespace RXApp.Model
{
    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime BornDate { get; set; }

        // список связанных выдач читателя
        public ICollection<Issue> Issues { get; set; }
    }
}
