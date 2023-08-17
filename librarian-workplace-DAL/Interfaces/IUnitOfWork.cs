using librarian_workplace_DAL.Interfaces.TypeInterfaces;

namespace librarian_workplace_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Book { get; }
        IReaderRepository Reader { get; }
        IReaderBookRepository ReaderBook { get; }
        Task<int> SaveAsync();
    }
}
