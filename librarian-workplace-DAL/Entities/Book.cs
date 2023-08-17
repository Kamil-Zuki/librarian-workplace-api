using librarian_workplace_DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace librarian_workplace_DAL.Entities
{
    public class Book : IDeletableEntity
    {
        public Book()
        {
            ReaderBooks = new HashSet<ReaderBook>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ArticleNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public int InstancesNumber { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ReaderBook>? ReaderBooks { get; set; }
    }
}
