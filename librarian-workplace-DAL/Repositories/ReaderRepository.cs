using librarian_workplace_DAL.EF;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces.TypeInterfaces;

namespace librarian_workplace_DAL.Repositories
{
    public class ReaderRepository : GenericRepository<Reader>, IReaderRepository
    {
        public ReaderRepository(LibraryContext context) : base(context) { }
    }
}
