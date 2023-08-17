using librarian_workplace_DAL.EF;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces.TypeInterfaces;

namespace librarian_workplace_DAL.Repositories
{
    public class ReaderBookRepository : GenericRepository<ReaderBook>, IReaderBookRepository
    {
        public ReaderBookRepository(LibraryContext context) : base(context) { }
    }
}
