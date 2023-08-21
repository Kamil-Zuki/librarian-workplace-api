namespace librarian_workplace_BLL.DTOs
{
    public class ReaderGiveDTO
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Guid>? ArticleNumbers { get; set; }
    }
}
