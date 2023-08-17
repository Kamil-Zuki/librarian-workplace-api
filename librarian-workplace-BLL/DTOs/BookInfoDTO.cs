namespace librarian_workplace_BLL.DTOs
{
    public class BookInfoDTO
    {
        public Guid ArticleNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public int InstancesNumber { get; set; }
        public List<Guid>? ReaderIds { get; set; }
    }
}
