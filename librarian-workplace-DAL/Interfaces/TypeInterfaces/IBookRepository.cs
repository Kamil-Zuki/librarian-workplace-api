using librarian_workplace_DAL.Entities;

namespace librarian_workplace_DAL.Interfaces.TypeInterfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        public IEnumerable<Book> ExtendedGet(Func<Book, bool> predicate);
    }
}
