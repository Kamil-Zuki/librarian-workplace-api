using librarian_workplace_DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace librarian_workplace_DAL.Entities
{
    public class ReaderBook : IDeletableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ReaderId { get; set; }
        public Guid ArticuleNumber { get; set; }
        public DateTime DateBorrowed { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
