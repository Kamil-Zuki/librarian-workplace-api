using librarian_workplace_DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace librarian_workplace_DAL.Entities
{
    public class Reader : IDeletableEntity
    {
        public Reader()
        {
            ReaderBooks = new HashSet<ReaderBook>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ReaderBook>? ReaderBooks { get; set; }
    }
}
