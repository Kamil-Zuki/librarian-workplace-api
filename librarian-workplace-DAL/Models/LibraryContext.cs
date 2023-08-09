using Microsoft.EntityFrameworkCore;

namespace librarian_workplace_DAL.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }


    }
}
